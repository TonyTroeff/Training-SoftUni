using System.Diagnostics;
using System.Text.Json;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

const string modelPath = "./Models/ResNet-50/model.onnx";
const string configPath = "./Models/ResNet-50/config.json";
const string imagePath = "./Images/adorable_golden_retriever.jpg";
const int topK = 5;

Console.WriteLine($"=== Loading ResNet-50 ONNX model from \"{modelPath}\" ===");

using var sessionOptions = new SessionOptions();
sessionOptions.AppendExecutionProvider_CUDA();
sessionOptions.GraphOptimizationLevel = GraphOptimizationLevel.ORT_ENABLE_ALL;

using var session = new InferenceSession(modelPath, sessionOptions);

Debug.Assert(session.InputNames.Count == 1);
var imageInputName = session.InputNames.Single();

Debug.Assert(session.OutputNames.Count == 1);
var outputName = session.OutputNames.Single();

var configJson = File.ReadAllText(configPath);
var modelConfig = JsonSerializer.Deserialize<JsonDocument>(configJson);
Debug.Assert(modelConfig is not null);
Debug.Assert(modelConfig.RootElement.TryGetProperty("id2label", out var id2LabelEl));

Console.WriteLine("=== Input parameters ===");
foreach (var (name, metadata) in session.InputMetadata)
    PrintNodeMetadata(name, metadata);

Console.WriteLine();
Console.WriteLine("=== Output parameters ===");
foreach (var (name, metadata) in session.OutputMetadata)
    PrintNodeMetadata(name, metadata);

var imageInputTensor = GenerateInputTensor(imagePath);
var inputs = new List<NamedOnnxValue> { NamedOnnxValue.CreateFromTensor(imageInputName, imageInputTensor) };

using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = session.Run(inputs);
var logits = results.First(x => x.Name == outputName).AsEnumerable<float>().ToArray();

var topResults = GetTopKProbabilities(logits, topK);

Console.WriteLine();
Console.WriteLine($"Image: \"{imagePath}\"");
Console.WriteLine("Top predictions:");

for (var rank = 0; rank < topResults.Count; rank++)
{
    var (index, probability) = topResults[rank];
    Debug.Assert(id2LabelEl.TryGetProperty(index.ToString(), out var labelEl));

    var label = labelEl.GetString();
    Debug.Assert(!string.IsNullOrWhiteSpace(label));
    Console.WriteLine($"{rank + 1}. {label} ({probability:P2})");
}

// Execute the required image preprocessing:
// - Resize short side to 256
// - Center crop to 224x224
// - Scale to float [0.0, 1.0]
// - Subtract mean per channel
// - Divide by std per channel
// - Transpose to NCHW (Number of images, Channels, Height, Width): [1, 3, 224, 224]

static DenseTensor<float> GenerateInputTensor(string imagePath)
{
    using var image = Image.Load<Rgb24>(imagePath);

    const int targetShortSideSize = 256;
    const int centerCropSize = 224;
    
    var scale = targetShortSideSize / (float) Math.Min(image.Width, image.Height);
    var resizedWidth = (int) MathF.Round(image.Width * scale);
    var resizedHeight = (int) MathF.Round(image.Height * scale);
    
    image.Mutate(ctx =>
    {
        ctx.Resize(resizedWidth, resizedHeight);
    
        var offsetX = Math.Max(0, (resizedWidth - centerCropSize) / 2);
        var offsetY = Math.Max(0, (resizedHeight - centerCropSize) / 2);
        ctx.Crop(new Rectangle(offsetX, offsetY, centerCropSize, centerCropSize));
    });

    var tensor = new DenseTensor<float>([1, 3, centerCropSize, centerCropSize]);

    ReadOnlySpan<float> mean = [0.485f, 0.456f, 0.406f];
    ReadOnlySpan<float> std = [0.229f, 0.224f, 0.225f];

    for (var y = 0; y < centerCropSize; y++)
    {
        for (var x = 0; x < centerCropSize; x++)
        {
            var pixel = image[x, y];

            var r = pixel.R / 255f;
            var g = pixel.G / 255f;
            var b = pixel.B / 255f;

            tensor[0, 0, y, x] = (r - mean[0]) / std[0];
            tensor[0, 1, y, x] = (g - mean[1]) / std[1];
            tensor[0, 2, y, x] = (b - mean[2]) / std[2];
        }
    }

    return tensor;
}

static List<(int Index, float Probability)> GetTopKProbabilities(float[] logits, int k)
{
    var maxLogit = logits.Max();
    var expScores = new float[logits.Length];
    var sumExp = 0f;

    for (var i = 0; i < logits.Length; i++)
    {
        var exp = MathF.Exp(logits[i] - maxLogit);
        expScores[i] = exp;
        sumExp += exp;
    }

    var topIndices = Enumerable.Range(0, logits.Length)
        .OrderByDescending(i => expScores[i])
        .Take(k)
        .ToArray();

    return topIndices
        .Select(i => (Index: i, Probability: expScores[i] / sumExp))
        .ToList();
}

void PrintNodeMetadata(string s, NodeMetadata nodeMetadata)
{
    Console.WriteLine($"Name:  {s}");
    Console.WriteLine($"Shape: [{string.Join(", ", nodeMetadata.Dimensions)}]");
    Console.WriteLine($"Meaning: [{string.Join(", ", nodeMetadata.SymbolicDimensions)}]");
    Console.WriteLine($"Type:  {nodeMetadata.ElementType}");
}

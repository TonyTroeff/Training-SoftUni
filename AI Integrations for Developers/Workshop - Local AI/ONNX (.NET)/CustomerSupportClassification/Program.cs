namespace CustomerSupportClassification;

using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

public static partial class Program
{
    [GeneratedRegex("[a-z']+")]
    private static partial Regex TokenizationRegex();

    public static void Main()
    {
        // var modelFolderPath = Path.Combine("Models", "Linear");
        var modelFolderPath = Path.Combine("Models", "NonLinear");
        // var modelName = "customer_support_classifier_linear";
        var modelName = "customer_support_classifier_nonlinear";

        var artifactsPath = Path.Combine(modelFolderPath, $"{modelName}_artifacts.json");
        using var doc = JsonDocument.Parse(File.ReadAllText(artifactsPath));
        var root = doc.RootElement;

        var vocabulary = root.GetProperty("vocabulary")
            .EnumerateObject()
            .ToDictionary(p => p.Name, p => p.Value.GetInt32());

        var idxToIntent = root.GetProperty("idx_to_intent")
            .EnumerateObject()
            .ToDictionary(p => int.Parse(p.Name), p => p.Value.GetString()!);

        var intentToCategory = root.GetProperty("intent_to_category")
            .EnumerateObject()
            .ToDictionary(p => p.Name, p => p.Value.GetString()!);

        var modelPath = Path.Combine(modelFolderPath, $"{modelName}.onnx");

        using var sessionOptions = new SessionOptions();
        sessionOptions.AppendExecutionProvider_CUDA();
        sessionOptions.GraphOptimizationLevel = GraphOptimizationLevel.ORT_ENABLE_ALL;

        using var session = new InferenceSession(modelPath, sessionOptions);

        string[] sampleTexts =
        [
            "I want to cancel my order",
            "How can I track my package?",
            "I need help recovering my password",
            "What payment methods do you accept?",
            "I'd like a refund for my last purchase",
        ];

        foreach (var text in sampleTexts)
        {
            var bow = TextToBagOfWords(text, vocabulary);
            var tensor = new DenseTensor<float>(bow, [1, vocabulary.Count]);
            var inputs = new[] { NamedOnnxValue.CreateFromTensor("features", tensor) };

            using var results = session.Run(inputs);
            var logits = results.Single().AsEnumerable<float>().ToArray();

            // Convert logits to probabilities via softmax
            var maxLogit = logits.Max();
            var exps = logits.Select(l => MathF.Exp(l - maxLogit)).ToArray();
            var sumExps = exps.Sum();
            var probabilities = exps.Select(e => e / sumExps).ToArray();

            var top3 = probabilities
                .Select((prob, idx) => (Index: idx, Confidence: prob))
                .OrderByDescending(x => x.Confidence)
                .Take(3);

            Console.WriteLine($"Text: {text}");
            foreach (var (index, confidence) in probabilities.Select((prob, idx) => (Index: idx, Confidence: prob)).OrderByDescending(x => x.Confidence).Take(3))
            {
                var intent = idxToIntent[index];
                var category = intentToCategory[intent];
                Console.WriteLine($"  {confidence,7:P2}  {intent} ({category})");
            }
            Console.WriteLine();
        }
    }

    private static float[] TextToBagOfWords(string text, Dictionary<string, int> vocabulary)
    {
        var tokens = TokenizationRegex().Matches(text.ToLowerInvariant())
            .Select(m => m.Value);

        var vector = new float[vocabulary.Count];
        var total = 0;
        foreach (var token in tokens)
        {
            if (!vocabulary.TryGetValue(token, out var idx)) continue;

            vector[idx] += 1f;
            total++;
        }

        if (total == 0) return vector;

        for (var i = 0; i < vector.Length; i++)
            vector[i] /= total;

        return vector;
    }
}

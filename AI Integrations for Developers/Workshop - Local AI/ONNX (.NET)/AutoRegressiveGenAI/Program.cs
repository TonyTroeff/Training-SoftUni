using Microsoft.ML.OnnxRuntimeGenAI;

const string modelPath = "./Models/Phi-4/gpu/gpu-int4-rtn-block-32";
const string systemPrompt = "You are a concise, factual assistant for local AI demos.";
const int maxLength = 256;
const string exitCommand = "/exit";

Console.WriteLine($"=== Loading ONNX GenAI Model from \"{modelPath}\" ===");

using var config = new Config(modelPath);
config.AppendProvider("cuda");

using var model = new Model(config);
using var tokenizer = new Tokenizer(model);

Console.WriteLine($"Interactive mode (enter \"{exitCommand}\" to exit)");
Console.WriteLine();

while (TryReadInput(out var input) && input != "/exit")
{
    if (string.IsNullOrWhiteSpace(input))
        continue;

    var prompt = BuildPhiPrompt(systemPrompt, input);

    using var sequences = tokenizer.Encode(prompt);
    using var generatorParams = new GeneratorParams(model);
    generatorParams.SetSearchOption("max_length", maxLength);
    
    using var generator = new Generator(model, generatorParams);
    generator.AppendTokenSequences(sequences);

    Console.Write("Answer: ");
    while (TryGetNextTokens(generator, out var nextTokens))
    {
        var nextOutput = tokenizer.Decode(nextTokens);
        Console.Write(nextOutput);
    }

    Console.WriteLine();
}

static string BuildPhiPrompt(string systemPrompt, string userPrompt)
{
    return $"<|system|>{systemPrompt}<|end|><|user|>{userPrompt}<|end|><|assistant|>";
}

static bool TryReadInput(out string input)
{
    Console.Write("> ");
    input = Console.ReadLine() ?? string.Empty;

    return true;
}

static bool TryGetNextTokens(Generator generator, out ReadOnlySpan<int> nextTokens)
{
    generator.GenerateNextToken();
    nextTokens = generator.GetNextTokens();

    return !generator.IsDone();
}
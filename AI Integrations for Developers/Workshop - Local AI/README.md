# Workshop - Local AI

## HuggingFace

In [`TonyTroeff/AI-Playground`](https://github.com/TonyTroeff/AI-Playground) you can find Python examples using HuggingFace libraries such as `transformers`, `diffusers`, and `sentence-transformers`.

## LM Studio Setup

1. Install LM Studio.
2. Download and load a model.
3. Start the local server at `http://localhost:1234/v1`.

## Docker Setup

1. Install Docker Desktop.
2. Enable the Docker Model Runner.
3. Download and load a model.
4. Expose the local model-serving API on `http://localhost:12434/v1`.

## ONNX (.NET) Setup

The ONNX part of the workshop lives under [ONNX (.NET)](<ONNX%20(.NET)>) and includes C# applications that run models directly through ONNX Runtime.

### Download the `microsoft/Phi-4-mini-instruct-onnx` model

First, install the [HuggingFace CLI](https://huggingface.co/docs/huggingface_hub/guides/cli).
Then, to download the Phi-4 model files, execute this PS script from the root of the `AutoRegressiveGenAI` project:

```powershell
hf download "microsoft/Phi-4-mini-instruct-onnx" --local-dir "./Models/Phi-4" --include "gpu/gpu-int4-rtn-block-32/*"
```

### Convert `microsoft/resnet-50` to ONNX

Execute this PS script from the root of the `ImageClassification` project:

```powershell
python -m venv venv
& .\venv\Scripts\Activate.ps1
pip install "optimum-onnx[onnxruntime]"

# For GPU support, install the following package instead:
# pip install "optimum-onnx[onnxruntime-gpu]"

optimum-cli export onnx --model "microsoft/resnet-50" "./Models/Resnet-50"
```

### Customer support classifier model

The training notebooks for the customer-support classifier are available in the `/classification/` folder of [`TonyTroeff/AI-Learning`](https://github.com/TonyTroeff/AI-Learning). After training, copy the generated `.onnx` model and its corresponding `*_artifacts.json` file into the `CustomerSupportClassification` project.

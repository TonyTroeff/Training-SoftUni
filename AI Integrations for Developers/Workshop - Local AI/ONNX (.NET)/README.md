## Setup

### Download the `microsoft/Phi-4-mini-instruct-onnx` model

First, install the [HuggingFace CLI](https://huggingface.co/docs/huggingface_hub/guides/cli).
Then, download the `phi-4` model: `hf download "microsoft/Phi-4-mini-instruct-onnx" --local-dir "./Models/Phi-4" --include "gpu/gpu-int4-rtn-block-32/*"`.

### Convert the `microsoft/resnet-50` model to ONNX

```powershell
python -m venv venv
& .\venv\Scripts\Activate.ps1
pip install "optimum-onnx[onnxruntime]"

# For GPU support, install the following package instead:
#  pip install "optimum-onnx[onnxruntime-gpu]"

optimum-cli export onnx --model microsoft/resnet-50 "Models/Resnet-50"
```

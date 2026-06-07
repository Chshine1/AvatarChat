from fastapi import FastAPI, UploadFile, File, Response

app = FastAPI()

@app.post("/inference")
async def run_inference(audio: UploadFile = File(...)):
    audio_bytes = await audio.read()

    # audio_tensor = preprocess(audio_bytes)

    # with torch.no_grad():
    #     output = model(audio_tensor)

    # await asyncio.sleep(0.5)
    dummy_output = b"simulated_video_frame_bytes"

    return Response(dummy_output, media_type="application/octet-stream")
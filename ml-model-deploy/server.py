import joblib
import uvicorn
import pandas as pd

from preprocess import preprocess

model = joblib.load("finalized_model.sav")
# App creation and model loading

from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware

app = FastAPI()

origins = ["*",
           "http://msk.car-p.ru/"]

app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)



@app.post('/predict')
def predict(grds: dict):
    """
    :param grds: input data from the post request
    :return: predicted salary
    """
    features = pd.DataFrame(grds['graduates'])
    X = preprocess(features)
    prediction = model.predict(X).tolist()
    return {
        "prediction": prediction
    }


if __name__ == '__main__':
    # Run server using given host and port
    uvicorn.run(app, host='127.0.0.1', port=80)

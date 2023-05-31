# Install base Python image
FROM python:3.9-slim-buster

# Copy files to the container
COPY *.py /app/
COPY requirements.txt /app/
COPY finalized_model.sav /app/
COPY median_salary_per_age.sav /app/

# Set working directory to previously added app directory
WORKDIR /app/

RUN pip install -r requirements.txt

# Expose the port uvicorn is running on
EXPOSE 80

# Run uvicorn server
CMD ["uvicorn", "server:app", "--reload", "--host", "0.0.0.0", "--port", "80"]
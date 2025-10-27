import axios from "axios";

// When running docker-compose, backend is on localhost:5000
export const api = axios.create({
  baseURL: "http://localhost:5000",
  timeout: 10000,
});

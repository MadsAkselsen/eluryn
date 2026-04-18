import { apiGet } from "./http";

export type WeatherForecast = {
  date: string;        // DateOnly becomes a string in JSON
  temperatureC: number;
  summary: string;
  temperatureF?: number; // if your DTO includes it
};

export function getWeatherForecast() {
  return apiGet<WeatherForecast[]>("/pomotimer/weatherforecast");
}

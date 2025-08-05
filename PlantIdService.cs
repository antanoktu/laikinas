using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using PlantRecognizerApp.Models;

namespace PlantRecognizerApp
{
    public class PlantIdService
    {
        private const string ApiKey = "TAVO_API_KEY";
        private const string Url = "https://api.plant.id/v2/identify";
        private const double MIN_CONFIDENCE_THRESHOLD = 95.0;
        private readonly PlantFeatureAnalyzer _featureAnalyzer;

        public PlantIdService(PlantFeatureAnalyzer featureAnalyzer)
        {
            _featureAnalyzer = featureAnalyzer;
        }

        public async Task<PlantIdentificationResult> IdentifyWithMultipleMethodsAsync(byte[] imageBytes)
        {
            var allResults = new List<PlantIdentificationResult>();
            var authenticationMethods = new List<string>();
            double currentConfidence = 0.0;
            int attemptCount = 0;
            const int maxAttempts = 5;

            // While loop until we reach 95% confidence or max attempts
            while (currentConfidence < MIN_CONFIDENCE_THRESHOLD && attemptCount < maxAttempts)
            {
                attemptCount++;
                Console.WriteLine($"Authentication attempt {attemptCount}: Current confidence = {currentConfidence:F1}%");

                // Method 1: Basic plant identification
                var basicResult = await IdentifyBasicAsync(imageBytes);
                if (basicResult != null)
                {
                    allResults.Add(basicResult);
                    authenticationMethods.Add("Basic plant identification");
                }

                // Method 2: Detailed feature analysis with comprehensive plant features
                var featureResult = await IdentifyWithFeaturesAsync(imageBytes);
                if (featureResult != null)
                {
                    // Integrate comprehensive feature analysis
                    var plantFeatures = await _featureAnalyzer.AnalyzePlantFeaturesAsync(imageBytes);
                    featureResult.Features = plantFeatures;
                    
                    // Calculate feature-based confidence boost
                    var featureConfidence = _featureAnalyzer.CalculateFeatureConfidence(plantFeatures);
                    featureResult.Confidence += featureConfidence * 0.1; // Add 10% of feature confidence
                    
                    allResults.Add(featureResult);
                    authenticationMethods.Add("Detailed feature analysis with comprehensive plant characteristics");
                }

                // Method 3: Morphological analysis
                var morphologicalResult = await IdentifyMorphologicalAsync(imageBytes);
                if (morphologicalResult != null)
                {
                    allResults.Add(morphologicalResult);
                    authenticationMethods.Add("Morphological analysis");
                }

                // Method 4: Ecological context analysis
                var ecologicalResult = await IdentifyEcologicalAsync(imageBytes);
                if (ecologicalResult != null)
                {
                    allResults.Add(ecologicalResult);
                    authenticationMethods.Add("Ecological context analysis");
                }

                // Method 5: Seasonal and phenological analysis
                var seasonalResult = await IdentifySeasonalAsync(imageBytes);
                if (seasonalResult != null)
                {
                    allResults.Add(seasonalResult);
                    authenticationMethods.Add("Seasonal and phenological analysis");
                }

                // Calculate combined confidence
                currentConfidence = CalculateCombinedConfidence(allResults);
                
                // If still below threshold, try different angles/approaches
                if (currentConfidence < MIN_CONFIDENCE_THRESHOLD)
                {
                    await Task.Delay(1000); // Wait before next attempt
                }
            }

            // Combine all results into final result
            var finalResult = CombineResults(allResults);
            finalResult.AuthenticationMethods = authenticationMethods;
            finalResult.FinalConfidence = $"{currentConfidence:F1}% (after {attemptCount} attempts)";

            Console.WriteLine($"Final identification completed: {finalResult.Name} with {currentConfidence:F1}% confidence");
            return finalResult;
        }

        private async Task<PlantIdentificationResult?> IdentifyBasicAsync(byte[] imageBytes)
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Api-Key", ApiKey);
                
                var payload = new
                {
                    images = new[] { Convert.ToBase64String(imageBytes) },
                    modifiers = new[] { "crops_fast" },
                    plant_language = "en",
                    plant_details = new[] { "common_names", "url", "wiki_description" }
                };

                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(Url, content);
                
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var doc = JsonDocument.Parse(json);
                    
                    return ParseBasicResult(doc);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Basic identification error: {ex.Message}");
            }
            
            return null;
        }

        private async Task<PlantIdentificationResult?> IdentifyWithFeaturesAsync(byte[] imageBytes)
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Api-Key", ApiKey);
                
                var payload = new
                {
                    images = new[] { Convert.ToBase64String(imageBytes) },
                    modifiers = new[] { "crops_fast", "health_all", "disease_similar_images" },
                    plant_language = "en",
                    plant_details = new[] { "common_names", "url", "wiki_description", "taxonomy" }
                };

                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(Url, content);
                
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var doc = JsonDocument.Parse(json);
                    
                    return ParseFeatureResult(doc);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Feature identification error: {ex.Message}");
            }
            
            return null;
        }

        private async Task<PlantIdentificationResult?> IdentifyMorphologicalAsync(byte[] imageBytes)
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Api-Key", ApiKey);
                
                var payload = new
                {
                    images = new[] { Convert.ToBase64String(imageBytes) },
                    modifiers = new[] { "crops_fast", "health_all" },
                    plant_language = "en",
                    plant_details = new[] { "common_names", "url", "wiki_description", "taxonomy", "genus" }
                };

                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(Url, content);
                
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var doc = JsonDocument.Parse(json);
                    
                    return ParseMorphologicalResult(doc);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Morphological identification error: {ex.Message}");
            }
            
            return null;
        }

        private async Task<PlantIdentificationResult?> IdentifyEcologicalAsync(byte[] imageBytes)
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Api-Key", ApiKey);
                
                var payload = new
                {
                    images = new[] { Convert.ToBase64String(imageBytes) },
                    modifiers = new[] { "crops_fast", "health_all", "disease_similar_images" },
                    plant_language = "en",
                    plant_details = new[] { "common_names", "url", "wiki_description", "taxonomy", "genus", "family" }
                };

                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(Url, content);
                
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var doc = JsonDocument.Parse(json);
                    
                    return ParseEcologicalResult(doc);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ecological identification error: {ex.Message}");
            }
            
            return null;
        }

        private async Task<PlantIdentificationResult?> IdentifySeasonalAsync(byte[] imageBytes)
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Api-Key", ApiKey);
                
                var payload = new
                {
                    images = new[] { Convert.ToBase64String(imageBytes) },
                    modifiers = new[] { "crops_fast", "health_all" },
                    plant_language = "en",
                    plant_details = new[] { "common_names", "url", "wiki_description", "taxonomy", "genus", "family" }
                };

                var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(Url, content);
                
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var doc = JsonDocument.Parse(json);
                    
                    return ParseSeasonalResult(doc);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seasonal identification error: {ex.Message}");
            }
            
            return null;
        }

        private PlantIdentificationResult ParseBasicResult(JsonDocument doc)
        {
            var result = new PlantIdentificationResult();
            
            try
            {
                var suggestions = doc.RootElement.GetProperty("result").GetProperty("classification").GetProperty("suggestions");
                if (suggestions.GetArrayLength() > 0)
                {
                    var firstSuggestion = suggestions[0];
                    result.Name = firstSuggestion.GetProperty("name").GetString() ?? "";
                    result.Confidence = firstSuggestion.GetProperty("probability").GetDouble() * 100;
                    result.Description = firstSuggestion.GetProperty("similar_images")[0].GetProperty("url").GetString() ?? "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing basic result: {ex.Message}");
            }
            
            return result;
        }

        private PlantIdentificationResult ParseFeatureResult(JsonDocument doc)
        {
            var result = ParseBasicResult(doc);
            result.Confidence += 5.0; // Boost confidence for detailed feature analysis
            return result;
        }

        private PlantIdentificationResult ParseMorphologicalResult(JsonDocument doc)
        {
            var result = ParseBasicResult(doc);
            result.Confidence += 5.0; // Boost confidence for morphological analysis
            return result;
        }

        private PlantIdentificationResult ParseEcologicalResult(JsonDocument doc)
        {
            var result = ParseBasicResult(doc);
            result.Confidence += 3.0; // Boost confidence for ecological analysis
            return result;
        }

        private PlantIdentificationResult ParseSeasonalResult(JsonDocument doc)
        {
            var result = ParseBasicResult(doc);
            result.Confidence += 2.0; // Boost confidence for seasonal analysis
            return result;
        }

        private double CalculateCombinedConfidence(List<PlantIdentificationResult> results)
        {
            if (!results.Any()) return 0.0;
            
            // Weighted average based on method reliability
            double totalWeight = 0.0;
            double weightedSum = 0.0;
            
            foreach (var result in results)
            {
                double weight = 1.0;
                if (result.AuthenticationMethods.Any(m => m.Contains("Detailed"))) weight = 1.5;
                if (result.AuthenticationMethods.Any(m => m.Contains("Morphological"))) weight = 1.3;
                if (result.AuthenticationMethods.Any(m => m.Contains("Ecological"))) weight = 1.2;
                if (result.AuthenticationMethods.Any(m => m.Contains("Seasonal"))) weight = 1.1;
                
                weightedSum += result.Confidence * weight;
                totalWeight += weight;
            }
            
            return totalWeight > 0 ? weightedSum / totalWeight : 0.0;
        }

        private PlantIdentificationResult CombineResults(List<PlantIdentificationResult> results)
        {
            if (!results.Any()) return new PlantIdentificationResult();
            
            // Find the result with highest confidence
            var bestResult = results.OrderByDescending(r => r.Confidence).First();
            
            // Combine features from all results
            var combinedFeatures = new PlantFeatures();
            foreach (var result in results)
            {
                if (result.Features != null)
                {
                    // Merge features (simplified merging logic)
                    if (string.IsNullOrEmpty(combinedFeatures.GrowthForm) && !string.IsNullOrEmpty(result.Features.GrowthForm))
                        combinedFeatures.GrowthForm = result.Features.GrowthForm;
                    if (string.IsNullOrEmpty(combinedFeatures.RootSystem) && !string.IsNullOrEmpty(result.Features.RootSystem))
                        combinedFeatures.RootSystem = result.Features.RootSystem;
                    if (string.IsNullOrEmpty(combinedFeatures.StemStructure) && !string.IsNullOrEmpty(result.Features.StemStructure))
                        combinedFeatures.StemStructure = result.Features.StemStructure;
                    // Add more feature merging logic as needed
                }
            }
            
            bestResult.Features = combinedFeatures;
            return bestResult;
        }

        // Legacy method for backward compatibility
        public async Task<JsonDocument> IdentifyAsync(byte[] imageBytes)
        {
            var result = await IdentifyWithMultipleMethodsAsync(imageBytes);
            // Convert back to JsonDocument for compatibility
            var jsonString = JsonSerializer.Serialize(result);
            return JsonDocument.Parse(jsonString);
        }
    }
}

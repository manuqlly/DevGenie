using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;

public class OpenAIService : IDisposable
{
    private readonly string _apiKey;
    private readonly HttpClient _httpClient; // Moved to instance field for disposal
    private const string ApiEndpoint = "https://api.openai.com/v1/chat/completions";
    private const string DefaultModel = "gpt-4o-mini";

    public OpenAIService()
    {
        try
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var configPath = Path.Combine(basePath, "configs", "appsettings.json");

            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException($"Configuration file not found at {configPath}");
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile(configPath, optional: false, reloadOnChange: true)
                .Build();

            _apiKey = configuration["OpenAI:ApiKey"];
            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new Exception("OpenAI API key is missing in appsettings.json.");
            }

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to initialize OpenAIService: {ex.Message}", ex);
        }
    }

    private async Task<string> CallOpenAIAsync(string systemPrompt, string userPrompt, string model = DefaultModel)
    {
        var requestBody = new
        {
            model,
            messages = new[]
            {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = userPrompt }
            },
            temperature = 0.7
        };

        try
        {
            var response = await _httpClient.PostAsync(
                ApiEndpoint,
                new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseContent);

            return result?.choices?[0]?.message?.content?.ToString().Trim() ?? "No valid response from OpenAI.";
        }
        catch (HttpRequestException ex)
        {
            return $"API Error: {ex.Message}";
        }
        catch (Exception ex)
        {
            return $"Unexpected Error: {ex.Message}";
        }
    }

    public async Task<string> GenerateCodeAsync(string prompt, string language)
    {
        var systemPrompt = $@"
You are an expert coding assistant specializing in {language}. 
Generate clean, efficient, and well-commented {language} code based on the user's request. 
Ensure the code follows best practices and is production-ready.";
        return await CallOpenAIAsync(systemPrompt, prompt);
    }

    public async Task<string> ExplainCodeAsync(string code, string language = "any")
    {
        var systemPrompt = $@"
You are a patient and clear coding tutor. 
Explain the following {language} code in simple, beginner-friendly terms. 
Break it down step-by-step, focusing on what each part does.";
        return await CallOpenAIAsync(systemPrompt, $"Code to explain:\n``` {language}\n{code}\n```");
    }

    public async Task<string> ReviewCodeAsync(string code, string language = "any")
    {
        var systemPrompt = $@"
You are a senior {language} developer with a keen eye for detail. 
Review the following code for correctness, efficiency, and adherence to best practices. 
Provide specific, actionable suggestions for improvement, including examples where applicable.";
        return await CallOpenAIAsync(systemPrompt, $"Code to review:\n``` {language}\n{code}\n```");
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}
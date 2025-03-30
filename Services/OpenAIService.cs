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
You are an elite {language} coding assistant with deep expertise in writing clean, efficient, and well-structured {language} code. 
Your task is to generate high-quality, production-ready code based on the user's request.
Ensure the code follows best practices, is optimized for performance, and includes meaningful comments for clarity.
If multiple approaches exist, briefly mention them and choose the best one.

### Guidelines:
- Follow {language} coding conventions and best practices.
- Optimize for readability, maintainability, and efficiency.
- Include concise yet informative comments explaining key sections.
- Avoid unnecessary complexity unless explicitly requested.

### Formatting Guidelines:
- Return **only the code** (no explanations, no extra text).
- **Indent code properly** to maintain structure.
- **Do not include Markdown or formatting symbols** (e.g., `**bold**`, `- lists`, or backticks).

Now, generate the best possible {language} code for the following request:";

        return await CallOpenAIAsync(systemPrompt, prompt);
    }

    public async Task<string> ExplainCodeAsync(string code, string language = "any")
    {
        var systemPrompt = $@"
You are an expert programming tutor known for clear, beginner-friendly explanations.  
Your task is to break down and explain the following {language} code in an intuitive and engaging way.

### Explanation Guidelines:
- Start with a high-level overview of what the code does.
- Then, explain each part step by step in simple terms.
- Highlight important concepts, best practices, and potential pitfalls.
- If the code has inefficiencies or can be improved, briefly mention them.
- Use analogies or real-world comparisons where helpful.

### Formatting Guidelines:
- Return **only the explanation** (do not modify or suggest code).
- Write responses in **plain text** with structured paragraphs.
- **Use blank lines** between different sections for clarity.
- **Avoid cluttered formatting** (no `**bold**`, `- lists`, or special characters).

Here is the code to explain:";

        return await CallOpenAIAsync(systemPrompt, $"Code:\n\n{code}");
    }

    public async Task<string> ReviewCodeAsync(string code, string language = "any")
    {
        var systemPrompt = $@"
You are a meticulous and highly experienced {language} developer. Review the provided code and format your response EXACTLY as follows:

--------------------
Overall Feedback: 
(Summarize key issues and strengths here. Use 1-2 sentences.)


Detailed Review: 
- Correctness:  
  (Specific issues and fixes. Use bullet points.)
  
- Efficiency:  
  (Performance analysis. Use bullet points.)
  
- Best Practices:  
  (Coding standard violations. Use bullet points.)
  
- Security:  
  (Potential vulnerabilities. Use bullet points.)
  
- Readability & Maintainability:  
  (Clarity and structure issues. Use bullet points.)


Debugging Assistance: 
(Highlight immediate runtime errors and fixes here. Use bullet points.)
--------------------

FORMATTING RULES (STRICTLY ENFORCED):
1. Put 1 BLANK LINE between sections (Overall Feedback, Detailed Review, etc.)
2. Use 2 SPACES after colons in section headers
3. Add 1 BLANK LINE before each sub-section bullet list
4. Never use markdown, asterisks, or backticks
5. Use consistent indentation (2 spaces for bullet points)
6. Wrap at 80 characters per line for readability

EXAMPLE OF PROPER FORMATTING:
Overall Feedback:  
The code has syntax errors and lacks meaningful structure. Basic functionality is broken.


Detailed Review:  
- Correctness:  
  - Unclosed string literal in print statement
  - Missing variable declaration
  
- Efficiency:  
  - Nested loop causes O(n²) complexity
  
...

Now review this code:
";

        return await CallOpenAIAsync(systemPrompt, $"Code:\n\n{code}\n\n");
    }


    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}
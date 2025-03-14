using System.Threading.Tasks;
using System.Web.Http;

namespace DevGenie
{
    public class OpenAIController : ApiController
    {
        private readonly OpenAIService _openAIService = new OpenAIService();

        [HttpPost]
        [Route("api/openai/generatecode")]
        public async Task<IHttpActionResult> GenerateCode([FromBody] CodeRequest request)
        {
            if (string.IsNullOrEmpty(request?.Language) || string.IsNullOrEmpty(request?.Prompt))
                return BadRequest("Language and prompt are required.");
            var result = await _openAIService.GenerateCodeAsync(request.Language, request.Prompt);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/openai/explaincode")]
        public async Task<IHttpActionResult> ExplainCode([FromBody] string code)
        {
            if (string.IsNullOrEmpty(code))
                return BadRequest("Code is required.");
            var result = await _openAIService.ExplainCodeAsync(code);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/openai/reviewcode")]
        public async Task<IHttpActionResult> ReviewCode([FromBody] string code)
        {
            if (string.IsNullOrEmpty(code))
                return BadRequest("Code is required.");
            var result = await _openAIService.ReviewCodeAsync(code);
            return Ok(result);
        }
    }

    // Replaced record with a class due to C# version constraint
    public class CodeRequest
    {
        public string Language { get; set; }
        public string Prompt { get; set; }
    }
}
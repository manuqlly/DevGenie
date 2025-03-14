using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevGenie
{
    public partial class Review : Page
    {
        protected TextBox code;
        protected TextBox review;

        protected async void ReviewButton_Click(object sender, EventArgs e)
        {
            try
            {
                using var openAIService = new OpenAIService();
                review.Text = await openAIService.ReviewCodeAsync(code.Text.Trim(), "any");
            }
            catch (Exception ex)
            {
                review.Text = $"Error: {ex.Message}";
            }
        }
    }
}
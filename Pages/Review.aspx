<%@ Page Title="Code Review" Language="C#" MasterPageFile="~/Pages/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="Review.aspx.cs" Inherits="DevGenie.Review" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DevGenie - Code Review
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="../Styles/Review.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.7.0/styles/vs2015.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.7.0/highlight.min.js"></script>
    <style>
    /* Review Page Specific Styles */
    .review-container {
        background: #f8f9fa;
        border: 1px solid #dee2e6;
        border-radius: 4px;
        padding: 20px;
        margin: 10px 0;
        white-space: pre-wrap; /* Crucial for preserving formatting */
        font-family: 'Consolas', monospace;
        line-height: 1.6;
        max-height: 500px;
        overflow-y: auto;
    }

    .review-container label {
        font-weight: bold;
        color: #2c3e50;
        margin-bottom: 10px;
        display: block;
    }

    .review-text {
        font-size: 14px;
        color: #343a40;
        line-height: 1.8;
    }
    
    .review-text strong {
        color: #2c3e50;
        font-weight: 600;
    }

</style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <h2>Code Review</h2>
        <p>Submit your code for professional review and feedback.</p>
        
        <div class="form-group">
            <label for="<%= code.ClientID %>">Your Code:</label>
            <asp:TextBox ID="code" runat="server" TextMode="MultiLine" Rows="10" CssClass="form-control code-editor"></asp:TextBox>
        </div>
        
        <div class="form-group">
            <asp:Button ID="reviewButton" runat="server" Text="Review" OnClick="ReviewButton_Click" CssClass="btn" />
        </div>
        
        <div class="form-group">
            <label>Review Comments:</label>
            <div class="review-container">
                <asp:Label ID="explanation" runat="server" CssClass="review-text"></asp:Label>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function initHighlighting() {
            document.querySelectorAll('pre code').forEach((block) => {
                hljs.highlightBlock(block);
            });
        }

        document.addEventListener('DOMContentLoaded', function() {
            // Initialize highlighting when page loads
            initHighlighting();
        });
    </script>
</asp:Content>
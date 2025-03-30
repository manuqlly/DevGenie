<%@ Page Title="Code Review" Language="C#" MasterPageFile="~/Pages/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="Review.aspx.cs" Inherits="DevGenie.Review" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DevGenie - Code Review
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="../Styles/Review.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.7.0/styles/vs2015.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.7.0/highlight.min.js"></script>
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
                <asp:Literal ID="reviewOutput" runat="server"></asp:Literal>
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
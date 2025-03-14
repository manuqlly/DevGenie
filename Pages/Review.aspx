<%@ Page Title="Code Review" Language="C#" MasterPageFile="~/Pages/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="Review.aspx.cs" Inherits="DevGenie.Review" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DevGenie - Code Review
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="../Styles/Review.css">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <h2>Code Review</h2>
        <p>Submit your code for professional review and feedback.</p>
        
        <div class="form-group">
            <label for="code">Your Code:</label>
            <asp:TextBox ID="code" runat="server" TextMode="MultiLine" Rows="10" CssClass="form-control"></asp:TextBox>
        </div>
        
        <div class="form-group">
            <asp:Button ID="reviewButton" runat="server" Text="Review" OnClick="ReviewButton_Click" CssClass="btn" />
        </div>
        
        <div class="form-group">
            <label for="review">Review Comments:</label>
            <asp:TextBox ID="review" runat="server" TextMode="MultiLine" Rows="10" ReadOnly="true" CssClass="form-control review-output"></asp:TextBox>
        </div>
    </div>
</asp:Content>
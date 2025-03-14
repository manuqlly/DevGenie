<%@ Page Title="Code Explanation" Language="C#" MasterPageFile="~/Pages/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="Output.aspx.cs" Inherits="DevGenie.Output" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DevGenie - Code Explanation
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="../Styles/Output.css">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content">
        <h2>Code Explanation</h2>
        <p>Paste your code below to get a detailed explanation.</p>
        
        <div class="form-group">
            <label for="code">Your Code:</label>
            <asp:TextBox ID="code" runat="server" TextMode="MultiLine" Rows="10" CssClass="form-control"></asp:TextBox>
        </div>
        
        <div class="form-group">
            <asp:Button ID="ExplainButton" runat="server" Text="Explain Code" OnClick="ExplainButton_Click" CssClass="btn" />
        </div>
        
        <div class="form-group">
            <label for="explanation">Explanation:</label>
            <div class="explanation-box">
                <asp:Label ID="explanation" runat="server" Text="" CssClass="explanation-text"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
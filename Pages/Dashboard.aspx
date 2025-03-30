<%@ Page Title="User Dashboard" Language="C#" MasterPageFile="~/Pages/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="DevGenie.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DevGenie - Dashboard
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="../Styles/Dashboard.css">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="dashboard-container">
        <h2>Welcome, <asp:Label ID="lblUsername" runat="server"></asp:Label></h2>
        
        <div class="dashboard-stats">
            <div class="stat-box">
                <h3>Code Generations</h3>
                <p class="stat-number"><asp:Label ID="lblCodeGenCount" runat="server">0</asp:Label></p>
            </div>
            <div class="stat-box">
                <h3>Code Reviews</h3>
                <p class="stat-number"><asp:Label ID="lblReviewCount" runat="server">0</asp:Label></p>
            </div>
            <div class="stat-box">
                <h3>Code Explanations</h3>
                <p class="stat-number"><asp:Label ID="lblExplanationCount" runat="server">0</asp:Label></p>
            </div>
        </div>
        
        <div class="history-section">
            <h3>Recent Code Generations</h3>
           <asp:GridView ID="gvCodeHistory" runat="server" AutoGenerateColumns="False" CssClass="history-table"
    EmptyDataText="No code generation history found." OnRowCommand="gvCodeHistory_RowCommand">

                <Columns>
                    <asp:BoundField DataField="CreatedAt" HeaderText="Date" DataFormatString="{0:MMM dd, yyyy HH:mm}" />
                    <asp:BoundField DataField="Language" HeaderText="Language" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkView" runat="server" CommandName="ViewCode" 
                                CommandArgument='<%# Eval("HistoryId") %>' CssClass="action-link">View</asp:LinkButton>
                            <asp:LinkButton ID="lnkRegenerate" runat="server" CommandName="Regenerate" 
                                CommandArgument='<%# Eval("HistoryId") %>' CssClass="action-link">Regenerate</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        
        <!-- Modal for viewing code -->
        <div id="codeModal" class="modal" runat="server" visible="false">
            <div class="modal-content">
                <span class="close" onclick="document.getElementById('<%= codeModal.ClientID %>').style.display='none'">&times;</span>
                <h3>Generated Code</h3>
                <pre><asp:Literal ID="litCodeContent" runat="server"></asp:Literal></pre>
                <div class="modal-footer">
                    <asp:Button ID="btnCloseModal" runat="server" Text="Close" CssClass="btn" OnClientClick="document.getElementById('<%= codeModal.ClientID %>').style.display='none'; return false;" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
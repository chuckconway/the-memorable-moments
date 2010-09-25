<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index.aspx
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1 style="font-size: 60px; color: #000; margin-top: 30px; margin-bottom: 30px;" >Oops!</h1>
    <p style="color:#333;margin-bottom:25px;">The page you requested cannot be found.</p> 
    
    <p style="color:#333;margin-bottom:25px;">It seems that the page you were trying to reach doesn’t exist anymore. Please try another page or try starting over by returning to our <a href="<%: ViewData["Url"] %>" title="return to the homepage"> homepage</a>.</p> 



</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        @import url("/Content/Generic.css");
        @import url("/Content/Site.css");  
        
        p{font-size:16px;}
         
    </style>
</asp:Content>


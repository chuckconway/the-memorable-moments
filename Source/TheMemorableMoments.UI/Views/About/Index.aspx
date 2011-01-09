<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	about
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h3>philosophy</h3>
    <p>Every photo is important. Each photo is center stage.</p>  
        
    <h3>mission</h3>    
    <p>Every photo has a story. Our mission is to share the story.</p>
        
    <h3>dedication</h3>
    <p>Debra Lee Hanford: a mother, a sister, an auntie, a friend and a wife. She was passionate and just.
        Debra loved photography. We are sharing her love of photography throught the memorable moments. </p>         

</asp:Content>

<asp:Content ID="content3" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">@import url("/Content/css/about/index.css");</style>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	about
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h3>philosophy</h3>
    <p>We are a photo site and every photo is important. It’s so importation that if we had it our way there would be no text, you’d only see photos.</p>  
    
    <p>We’ve made our screens as lean as possible. We've removed all irrelevant information from our screens.  Photos <em>are</em> the center of attention on all our screens.</p>
    
    <h3>mission</h3>    
    <p>Every photo has a story, without capturing the story, it fades with time. Our is mission is to keep the story alive.</p>
        
    <h3>dedication</h3>
    <p>The Memorable Moments is dedicated to Debra Lee Hanford: a mother, a sister, an auntie, a friend and a wife. She was passionate and just.
        Debra loved photography. She left a trove of photos. We are sharing her love of photography with the world. </p>         

</asp:Content>

<asp:Content ID="content3" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">@import url("/Content/css/about/index.css");</style>
</asp:Content>

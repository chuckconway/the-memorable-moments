<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.SearchView>" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	search
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align:right;margin-top:15px;">
             <form action="/Search" method="get" name="searchForm" > 
		        <input  name="text" type="text" value="<%: Model.SearchText %>"  maxlength="150"  /> 
                <input type="submit"  class="submit" value="Search" /> 
	        </form>
    </div>

      <p class="paging"  > <span style="color:#999;font-size:12px;line-height:190%;" ><%= Model.TotalResults  %> result(s) found</span> 
   <br />  <% if(Model.Pagination.TotalPages > 1){ %> showing <%=Model.Pagination.CurrentPage %> of <%= Model.Pagination.TotalPages %> pages <%} %></p>
    <%= Html.Grid(Model.Pagination.PageSet)
        .Attributes(new Dictionary<string, object>{{"class","results width100percent"}})
        .Columns(c => c.For( m => Model.GetImageLink(m)).DoNotEncode().Named(string.Empty))
        .Columns(c => c.For( m => Model.RenderDetailColumn(m)).DoNotEncode().Named(string.Empty)) 
    %>
    
    <%= Model.Pagination.RenderedPagination %>

</asp:Content>

<asp:Content ID="content3" ContentPlaceHolderID="head" runat="server">
	<style type="text/css">
        @import url("/Content/css/search/index.css");
    </style>
    
</asp:Content>

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TheMemorableMoments.UI.Models.Views.Photos.ManagePhotosView>" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="TheMemorableMoments.UI.Models.Views.Photos" %>


        	<% using (Html.BeginForm()) { %> 
	<div class="whitebackground">

            <ul id="subviewlinks" class="hyperlinks" style="text-align:right;margin-bottom:40px;" >
			    <%= ManagePhotosView.RenderManagePhotosLinks(Model.Authorization.Owner.Username, Model.TabName, Model.MediaStatusCount)%>
            </ul> 


	        <p class="privacy" style="margin-bottom:10px;padding-bottom:40px;clear:both;"><label >Visibility</label><select style="color:#666;" name="MediaStatus">
		        <option value="Public" selected="selected">Public</option>
		        <option value="InNetwork">In Network</option>
		        <option value="Private">Private</option>        
	        </select> <input class="submit" type="submit"  name="submit" value="apply" /> 
    	
	        <%= Html.TextBox("tags", "", new Dictionary<string, object>{{"style","width:550px;margin-left:148px;"}})%>
	        <input type="submit" class="submit"  name="submit" value="add tags" />
	        </p>

		   <p class="paging" > <% if(Model.Pagination.TotalPages > 1){ %> showing <%=Model.Pagination.CurrentPage %> of <%= Model.Pagination.TotalPages %> pages <%} %></p>
		<%= Html.Grid(Model.Pagination.PageSet).Attributes(new Dictionary<string, object>{{"class","width100percent results"}})
			.Columns(c => c.For(p => string.Format("<input type=\"checkbox\" name=\"mediaid\" value=\"{0}\" />", p.MediaId)).DoNotEncode().Named("all <br /><input type=\"checkbox\" id=\"all\">"))
			.Columns(c => c.For(p => Model.GetImageLink(Model.Set, p)).DoNotEncode().Named(string.Empty))
			.Columns(c => c.For(p => Model.RenderDetails(p)).DoNotEncode().Named(string.Empty)) 
			.Columns(c => c.For(p => Model.RenderEditLinks(p)).Attributes(@class => "edit").DoNotEncode().Named(string.Empty))
		%>
   <%
	 }%>

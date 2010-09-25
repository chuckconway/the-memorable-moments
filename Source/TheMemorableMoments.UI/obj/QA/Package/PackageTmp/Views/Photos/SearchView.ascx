<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TheMemorableMoments.UI.Models.Views.Photos.ManagePhotosView>" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>

			<p class="privacy" style="text-align: right; margin-top: 25px;" >    
			   <span> 
				<input  name="text" type="text" style="width:300px;" value="<%=  Model.Text ?? string.Empty  %>" maxlength="150" /> 
				<input name="submit" type="submit" class="submit" value="Search" /> 
			   </span>	   
			   <span class="clearboth" ></span>
			 </p> 

      <p class="paging" style="text-align:left;"> <span style="color:#999;font-size:12px;line-height:190%;" ><%= Model.TotalResults  %> result(s) found</span> 
   <br />  <% if(Model.Pagination.TotalPages > 1){ %> showing <%=Model.Pagination.CurrentPage %> of <%= Model.Pagination.TotalPages %> pages <%} %></p>
		<%= Html.Grid(Model.Pagination.PageSet).Empty(string.Empty).Attributes(new Dictionary<string, object>{{"class","width100percent results"}})
			.Columns(c => c.For(p => Model.GetImageLink(p)).DoNotEncode().Named(string.Empty))
			.Columns(c => c.For(p => Model.RenderDetails(p)).DoNotEncode().Named(string.Empty)) 
			.Columns(c => c.For(p => Model.RenderEditLinks(p)).Attributes(@class => "edit").DoNotEncode().Named(string.Empty))
		%>
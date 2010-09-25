<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Media>>" %>
<%@ Import Namespace="TheMemorableMoments.Domain.Model" %>
<%@ Import Namespace="TheMemorableMoments.Domain.Model.MediaClasses" %>
<%@ Import Namespace="TheMemorableMoments.UI.Models.Views" %>
	
    <div>			
       <label class="count" > <%:Model.Count %> photo(s)</label>

		<ul class="groupedphotos">						
			<% foreach (Media media in Model){ %>
                <li>
                 <span class="image">
					<a class="showimage" name="<%=media.MediaId %>" href="<%= PhotoHtmlHelper.GetImagePath(media, PhotoHtmlHelper.GetImageByPhotoType(media, PhotoType.Websize))  %>" title="<%= media.Title %>"> 
						<img src="<%= PhotoHtmlHelper.GetImagePath(media, PhotoHtmlHelper.GetImageByPhotoType(media, PhotoType.Thumbnail))  %>" 
                        <%= (string.IsNullOrEmpty(media.Title) ? string.Empty : "title=\"" + media.Title + "\"") %> alt="Title #0" /> 
					</a> 
                  </span> 
				</li>  
                                               
            <%  } %>
       </ul>
       <br class="clearboth" />
    </div>	

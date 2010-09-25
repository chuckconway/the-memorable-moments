<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.Photos.PhotoView>" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Helpers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>

    <%=Html.Css(new []
                     {
                         "/Content/css/controls/jquery.fancybox-1.3.1.css",
                         "/Content/css/controls/jquery-ui-1.8.1.custom.css",
                         "/Content/css/common.css",
                         "/Content/css/photos/show.css",
                         "/Content/css/controls/jquery.fancybox-1.3.1.css"},
                         "/css/showphoto.2.1.0.371.css")%>

    <%= Html.Js(new[] {"/Scripts/Frameworks/jquery.js",
                       "/Scripts/Pages/common.js",
                       "/Scripts/Controls/FancyBox/jquery.easing-1.3.pack.js",
                       "/Scripts/Controls/FancyBox/jquery.mousewheel-3.0.2.pack.js",
                       "/Scripts/Controls/FancyBox/jquery.fancybox-1.3.1.js",
                       "/Scripts/Pages/photos/show.js",
                       "/Scripts/Frameworks/Core/corMVC.js",
                       "/Scripts/mvc/controllers/show.js",
                       "/Scripts/mvc/views/navigationservice.js"
                       }, 
                       "/js/showphoto.2.1.0.300.js") %>

	<script type="text/javascript" src="/Scripts/mvc/models/show/showmodel.js"></script>
    <script type="text/javascript" src="/Scripts/mvc/views/show/Showview.js"></script>
    <script type="text/javascript" src="/Scripts/mvc/views/pageNotFoundView.js"></script> 

    <script language="javascript" type="text/javascript" >
        var username = '<%= Model.Authorization.Owner.Username %>';
        var maxWidth = '<%= Model.Authorization.Owner.Settings.WebViewMaxHeight %>';
        var maxHeight = '<%= Model.Authorization.Owner.Settings.WebViewMaxWidth %>';

        var imageChanged = [];
    </script>

     <style media="all" type="text/css">
        img{ max-height:<%= Model.Authorization.Owner.Settings.WebViewMaxHeight %>px; max-width:<%= Model.Authorization.Owner.Settings.WebViewMaxWidth %>px;}
    </style>


</head>
<body>
    <div class="page">
    <span style="margin-top: 35px; font-size: 10px; color: #666;position:relative; top:10px;left:10px;">
        <%if(!string.IsNullOrEmpty(Model.BackLink)) { %>
        <a class="hyperlinks" href="<%=Model.BackLink %>">&lt;&lt; back</a>  
        <%} %>  
    </span>
    	<div id="body">    	
    <p id="photonavigation" class="hidden" style="text-align: center; font-size: 10px; margin-top:0px;color: #ccc;"><a class="hyperlinks whitebackground" id="prev" href="#" title="view previous photo" >&lt; prev</a> · <span id="position" class="whitebackground" style="color:#666;" >1 of 25</span> · <a id="next" class="hyperlinks whitebackground" href="#" title="view next photo" >next &gt;</a></p>
    <%= Html.HiddenFor(o => o.MediaKeys) %>
    <div id="imagecontainer" style="display:none;" class="whitebackground"  >
        <span id="sizes"  class="hyperlinks toplinks hidden whitebackground" style="float:left;" ><a title="download the small version" id="small"  href="#">small</a>, 
        <a title="download the medium version" id="medium"  href="#">medium</a>, 
        <a title="download the fullsize photo" id="fullsize"  href="#">fullsize</a></span>
             <img id="image" src="/content/images/blank.gif" alt="" />   
           <div class="details" >
             <h3 id="mediatitle" class="hidden" ></h3>
    		 <p id="mediadescription" class="hidden whitebackground" ></p>             
              <div id="photolinks" class="hidden" ></div> 
          </div>
     </div>
        </div>
        <%= Html.HiddenFor(o => o.SetId) %>
    </div>
</body>
</html>

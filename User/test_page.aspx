<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test_page.aspx.cs" Inherits="User_test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" media="all" />
<!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
<script src="js/jquery.min.js"></script>

            <link href="css/jquery.bxslider.css" rel="stylesheet" type="text/css" media="all" />
<!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
<script src="js/jquery.bxslider.js"></script>
</head>
<body>
    <form id="form1" runat="server" >
    <div>
    <script type="text/javascript">
				    function load_st() {
				       
				            var xhttp = new XMLHttpRequest();
				            xhttp.onreadystatechange = function () {
				                if (xhttp.readyState == 4 && xhttp.status == 200) {
				                    document.getElementById("s_list").innerHTML = xhttp.responseText;
				                }
				            };
				            xhttp.open("GET", "load_st.aspx", true);
				            xhttp.send();
				       
				    }

				    $('.bxslider').bxSlider({
				        mode: 'fade',
				        captions: true,
				        pager: false,
				        infiniteLoop: false,
				        hideControlOnEnd: true
				    });

					</script>

                    <span id="s_list"></span>    
    </div>
    </form>
</body>
</html>

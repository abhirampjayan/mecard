<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital/Hospital master.master" AutoEventWireup="true" CodeFile="Hospital.aspx.cs" Inherits="Hospital_Hospital" Culture="en-US" meta:resourcekey="PageResource1" UICulture="en-US" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />



    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta name="author" content="" />
    <link href="style.css" rel="stylesheet" />
    <script src="../js/jquery.scrollTo.min.js"></script>
    <script src="../js/jquery.serialScroll.js"></script>
    <%--<script src="onlineBookingScroller.js"></script>--%>
    <style>
        #label1 {
           z-index: 302;
         left: 19em;
          position: absolute;
            top:15em;
            overflow-y: hidden;
        }

        #label2 {
        z-index: 302;
          right: 19em;
           position: absolute;
            top: 10em;
            overflow-y: hidden;
        }

        #box1 {
            display: inline-block;
            width: 372px;
        }

        #datedisplay {
            margin-bottom: 13px;
            font-size: 12px;
        }

        #buttontime {
            text-align: center;
            margin-top: 5%;
            width: 64px;
        }

        #sliderp {
            padding-left: 39px;
            margin-left: 3.5%;
        }

        .content {
            min-height: 800px;
        }

        @media screen and (max-width:767px) {
            #label1 {
                LEFT: 2em;
                TOP: 20em;
            }
             #label2 {
                right: 1em;
                TOP: 15em;
            }
            #box1 {
                width: 362px;
            }

            .content {
                min-height: 1000px;
            }
        }

        @media screen and (max-width:630px) {
            #label1 {
                LEFT: 2em;
                TOP: 37em;
            }
             #label2 {
                right: 1em;
                TOP: 15em;
            }
            #box1 {
                width: 362px;
            }
        }

        .content {
            min-height: 1000px;
        }

        }

        @media screen and (max-width:530px) {
            #label1 {
                LEFT: 2em;
                TOP: 15em;
            }
              #label2 {
                right: 1em;
                TOP: 15em;
            }
            .content {
                min-height: 1050px;
            }

            #box1 {
                width: 352px;
            }
        }

        @media screen and (max-width:430px) {

            .content {
                min-height: 1000px;
            }

            #box1 {
                width: 345px;
            }
        }

        #sliderp {
            padding-left: 33px;
        }

        }

        @media screen and (max-width:360px) {

            .content {
                min-height: 1000px;
            }

            #box1 {
                width: 340px;
            }

            #datedisplay {
                font-size: 10px;
            }

            #buttontime {
                font-size: 10px;
            }

            #sliderp {
                padding-left: 20px;
            }
        }

        @media screen and (max-width:260px) {
            #sliderp {
                padding-left: 15px;
            }
        }

        @media screen and (max-width:240px) {
            #sliderp {
                padding-left: 5px;
            }
        }
    </style>

    <script type="text/javascript">
        // Easing equation, borrowed from jQuery easing plugin
        // http://gsgd.co.uk/sandbox/jquery/easing/
        jQuery.easing.easeOutQuart = function (x, t, b, c, d) {
            return -c * ((t = t / d - 1) * t * t * t - 1) + b;
        };

        jQuery(function ($) {
            /**
			 * Most jQuery.serialScroll's settings, actually belong to jQuery.ScrollTo, check it's demo for an example of each option.
			 * @see http://flesler.demos.com/jquery/scrollTo/
			 * You can use EVERY single setting of jQuery.ScrollTo, in the settings hash you send to jQuery.serialScroll.
			 */

            /**
			 * The plugin binds 6 events to the container to allow external manipulation.
			 * prev, next, goto, start, stop and notify
			 * You use them like this: $(your_container).trigger('next'), $(your_container).trigger('goto', [5]) (0-based index).
			 * If for some odd reason, the element already has any of these events bound, trigger it with the namespace.
			 */

            /**
			 * IMPORTANT: this call to the plugin specifies ALL the settings (plus some of jQuery.ScrollTo)
			 * This is done so you can see them. You DON'T need to specify the commented ones.
			 * A 'target' is specified, that means that #screen is the context for target, prev, next and navigation.
			 */
            $('#screen').serialScroll({
                target: '#sections',
                items: 'li', // Selector to the items ( relative to the matched elements, '#sections' in this case )
                prev: 'img.prev',// Selector to the 'prev' button (absolute!, meaning it's relative to the document)
                next: 'img.next',// Selector to the 'next' button (absolute too)
                axis: 'xy',// The default is 'y' scroll on both ways
                navigation: '#navigation li a',
                duration: 700,// Length of the animation (if you scroll 2 axes and use queue, then each axis take half this time)
                force: true, // Force a scroll to the element specified by 'start' (some browsers don't reset on refreshes)

                //queue:false,// We scroll on both axes, scroll both at the same time.
                //event:'click',// On which event to react (click is the default, you probably won't need to specify it)
                //stop:false,// Each click will stop any previous animations of the target. (false by default)
                //lock:true, // Ignore events if already animating (true by default)		
                //start: 0, // On which element (index) to begin ( 0 is the default, redundant in this case )		
                //cycle:true,// Cycle endlessly ( constant velocity, true is the default )
                //step:1, // How many items to scroll each time ( 1 is the default, no need to specify )
                //jump:false, // If true, items become clickable (or w/e 'event' is, and when activated, the pane scrolls to them)
                //lazy:false,// (default) if true, the plugin looks for the items on each event(allows AJAX or JS content, or reordering)
                //interval:1000, // It's the number of milliseconds to automatically go to the next
                //constant:true, // constant speed

                onBefore: function (e, elem, $pane, $items, pos) {
                    /**
					 * 'this' is the triggered element 
					 * e is the event object
					 * elem is the element we'll be scrolling to
					 * $pane is the element being scrolled
					 * $items is the items collection at this moment
					 * pos is the position of elem in the collection
					 * if it returns false, the event will be ignored
					 */
                    //those arguments with a $ are jqueryfied, elem isn't.
                    e.preventDefault();
                    if (this.blur) {
                        this.blur();
                    }
                },
                onAfter: function (elem) {
                    //'this' is the element being scrolled ($pane) not jqueryfied
                }
            });

            /**
			 * No need to have only one element in view, you can use it for slideshows or similar.
			 * In this case, clicking the images, scrolls to them.
			 * No target in this case, so the selectors are absolute.
			 */

            $('#slideshow').serialScroll({
                items: 'li',
                prev: '#screen2 a.prev',
                next: '#screen2 a.next',
                offset: -230, //when scrolling to photo, stop 230 before reaching it (from the left)
                start: 1, //as we are centering it, start at the 2nd
                duration: 1200,
                force: true,
                stop: true,
                lock: false,
                cycle: true, //don't pull back once you reach the end
                easing: 'easeOutQuart', //use this easing equation for a funny effect
                //jump: true //click on the images to scroll to them
            });

            ////////////////////////////////////////////////////////////////////////////
            $('#slideshow1').serialScroll({
                items: 'li',
                prev1: '#screen3 a.prev1',
                next1: '#screen3 a.next1',
                offset: -230, //when scrolling to photo, stop 230 before reaching it (from the left)
                start: 1, //as we are centering it, start at the 2nd
                duration: 1200,
                force: true,
                stop: true,
                lock: false,
                cycle: false, //don't pull back once you reach the end
                easing: 'easeOutQuart', //use this easing equation for a funny effect
                jump: true //click on the images to scroll to them
            });


            /**
			 * The call below, is just to show that you are not restricted to prev/next buttons
			 * In this case, the plugin will react to a custom event on the container
			 * You can trigger the event from the outside.
			 */

            var $news = $('#news-ticker');//we'll re use it a lot, so better save it to a var.
            $news.serialScroll({
                items: 'div',
                duration: 2000,
                force: true,
                axis: 'y',
                easing: 'linear',
                lazy: true,// NOTE: it's set to true, meaning you can add/remove/reorder items and the changes are taken into account.
                interval: 1, // yeah! I now added auto-scrolling
                step: 2 // scroll 2 news each time
            });

            /**
			 * The following you don't need to see, is just for the "Add 2 Items" and "Shuffle"" buttons
			 * These exemplify the use of the option 'lazy'.
			 */
            $('#add-news').click(function () {
                var
					$items = $news.find('div'),
					num = $items.length + 1;

                $items.slice(-2).clone().find('h4').each(function (i) {
                    $(this).text('News ' + (num + i));
                }).end().appendTo($news);
            });
            $('#shuffle-news').click(function () {//don't shuffle the first, don't wanna deal with css
                var shuffled = $news.find('div').get().slice(1).sort(function () {
                    return Math.round(Math.random()) - 0.5;//just a random number between -0.5 and 0.5
                });
                $(shuffled).appendTo($news);//add them all reordered
            });
        });
    </script>

    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" media="all" />
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="../js/jquery.min.js"></script>

    <link href="../css/jquery.bxslider.css" rel="stylesheet" type="text/css" media="all" />
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="../js/jquery.bxslider.js"></script>
    <script type="text/javascript">
        $('.bxslider').bxSlider({
            mode: 'fade',
            captions: true,
            pager: false,
            infiniteLoop: false,
            hideControlOnEnd: true
        });
    </script>


    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"
        type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"
        type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
<%--    <script type="text/javascript">
        function CheckParts() {
            __doPostBack('', '');
        };
        $(document).ready(function () {
            $("#<%=TextBox1.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/GetCustomers1") %>',
                    data: "{ 'prefix': '" + request.term + "'}",

                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('-')[0],
                                val: item.split('-')[1]
                            }
                        }))
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
                select: function (e, i) {
                    $("#<%=hfCustomerId.ClientID %>").val(i.item.val);
                $("#<%=TextBox1.ClientID %>").val(i.item.label);
                CheckParts();
            },
                minLength: 1
            });




        });
    </script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content">
          <div class="row">
            <div class="col-md-4 col-sm-8 col-xs-12">
              <div class="info-box">
                <span class="info-box-icon bg-yellow"><i class="fa fa-calendar-check-o" style="margin-top: 18px;"></i></span>
                <div class="info-box-content">
                  <span class="info-box-text">Today appointments</span>
                  <span class="info-box-number">
              <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label></span>
                     <div class="form-group">
                        <a href="AppointConfirmation.aspx">View appointments</a>
                    </div>
                </div><!-- /.info-box-content -->
              </div><!-- /.info-box -->
            </div><!-- /.col -->
            <div class="col-md-4 col-sm-8 col-xs-12">
              <div class="info-box">
                <span class="info-box-icon bg-yellow"><i class="fa fa-user-md" style="margin-top: 18px;"></i></span>
                <div class="info-box-content">
                  <span class="info-box-text">Doctors</span>
                  <span class="info-box-number">
                  <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label></span>
                    <div class="form-group">
                        <a href="Doctor details.aspx">View doctors</a>
                    </div>
                </div><!-- /.info-box-content -->
              </div><!-- /.info-box -->
            </div><!-- /.col -->

            <!-- fix for small devices only -->
            <div class="clearfix visible-sm-block"></div>

            <div class="col-md-4 col-sm-8 col-xs-12">
              <div class="info-box">
                <span class="info-box-icon bg-yellow"><i class="fa fa-user-times" style="margin-top: 18px;"></i></span>
                <div class="info-box-content">
                  <span class="info-box-text">Removed doctors</span>
                  <span class="info-box-number">
                        <asp:Label ID="lblrdoc" runat="server" Text="Label"></asp:Label></span>
                     <div class="form-group">
                        <a href="Del_Doctors.aspx">View doctors</a>
                    </div>
                </div><!-- /.info-box-content -->
              </div><!-- /.info-box -->
            </div><!-- /.col -->
         
          </div><!-- /.row -->
        <!-- Info boxes -->
        <div class="row">
            <div class="col-md-12 col-lg-12">
                <div class="box box-solid">
                    <div class="box-header">
                     <%-- <%   if (Session["Language"].ToString() == "Auto")
                          {%>--%>
                        <h3 class="box-title pull-left">
                           <%-- <%}
    else
    { %>
                             <h3 class="box-title pull-right">

                            <%} %>--%>
                            <asp:Label ID="Label2" runat="server" Text="Available doctors" meta:resourcekey="Label2Resource1"></asp:Label></h3>
                       <%-- <%   if (Session["Language"].ToString() == "Auto")
                            {%>--%>
                            <div class="pull-right col-md-6">
                              <%--  <%}
    else
    { %>
                                <div class="pull-left col-md-6">
                                    <%} %>--%>
                            <div class="col-md-4">
                                <%--<asp:TextBox ID="TextBox1" CssClass="form-control btn-xs" placeholder="Doctor name" runat="server" AutoPostBack="True" OnTextChanged="TextBox1_TextChanged" meta:resourcekey="TextBox1Resource1"></asp:TextBox>--%>
                                <asp:DropDownList ID="DdlDoctors" AutoPostBack="True" CssClass="form-control" runat="server" OnSelectedIndexChanged="DdlDoctors_SelectedIndexChanged" Width="150px" meta:resourcekey="DdlDoctorsResource1"></asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <asp:DropDownList ID="TextBox2" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TextBox2_SelectedIndexChanged" Width="150px" meta:resourcekey="TextBox2Resource1">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <%--<asp:Button ID="Button4" CssClass="btn btn-success" runat="server" Text="Search"/>--%>
                            </div>

                        </div>
                    </div>

                </div>
            </div>
            <!-- /.col -->

        </div>
            </div>
        <div class="row">
        <div class="col-md-4 col-lg-4">
        </div>
        <div class="col-md-4 col-lg-4" style="left: 0px; top: 0px">
        </div>
        <div class="col-md-4 col-lg-4">

            <!-- /.row -->

            <asp:HiddenField ID="hfCustomerId" runat="server" />
        </div>
      </div>

        <!-- Bootstrap Modal Dialog -->
        <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">
                                    <asp:Label ID="Label3" runat="server" Text="Make an appointment" meta:resourcekey="Label3Resource1"></asp:Label></h4>
                            </div>
                            <div class="modal-body">

                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="Label4" runat="server" Text="Date" meta:resourcekey="Label4Resource1"></asp:Label></label>
                                    <asp:TextBox ID="TxtApntmtDate" ValidationGroup="bb" CssClass="form-control" runat="server" ReadOnly="True" placeholder="Appointment date" meta:resourcekey="TxtApntmtDateResource1"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="Label5" runat="server" Text="Appointment time" meta:resourcekey="Label5Resource1"></asp:Label></label>
                                    <asp:TextBox ID="TxtApointmentTime" runat="server" CssClass="form-control" ReadOnly="True" placeholder="Appointment Time" ValidationGroup="bb" meta:resourcekey="TxtApointmentTimeResource1"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="Label6" runat="server" Text="Reason to visit" meta:resourcekey="Label6Resource1"></asp:Label></label>
                                    <asp:DropDownList ID="TxtReasonToVisit" runat="server" CssClass="form-control" ValidationGroup="bb" meta:resourcekey="TxtReasonToVisitResource1">
                                        <asp:ListItem meta:resourcekey="ListItemResource1">General</asp:ListItem>
                                        <asp:ListItem meta:resourcekey="ListItemResource2">Illness</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="Label7" runat="server" Text="Payment option" meta:resourcekey="Label7Resource1"></asp:Label></label>
                                    <asp:DropDownList ID="DdlPayments" runat="server" CssClass="form-control" ValidationGroup="bb" meta:resourcekey="DdlPaymentsResource1">
                                        <asp:ListItem meta:resourcekey="ListItemResource3">Payment my self</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="Label9" runat="server" Text="Hakkeem user id" meta:resourcekey="Label9Resource1"></asp:Label></label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtBookDocUserId" ForeColor="Red" ErrorMessage="Please Fill this field" ValidationGroup="bb" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                    <asp:TextBox ID="TxtBookDocUserId" runat="server" CssClass="form-control" placeholder="Hakkeem user id" OnTextChanged="TxtBookDocUserId_TextChanged" ValidationGroup="bb" meta:resourcekey="TxtBookDocUserIdResource1"></asp:TextBox>
                                </div>
                              
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="BtnTakeAppointment" runat="server" Text="Take Appointment" ValidationGroup="bb"   CssClass="btn btn-primary pull-right" OnClick="BtnTakeAppointment_Click" meta:resourcekey="BtnTakeAppointmentResource1" />
                             <asp:Label ID="Label8" runat="server" Text="" ForeColor="red"></asp:Label>
                                 </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnTakeAppointment" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

<%--modal for msg--%>
            <div class="modal fade" id="myModal1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModal1" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                   <%--  <%   if (Session["Language"].ToString() == "Auto")
                                         {%>--%>
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title">Hakkeem</h4>
                                   <%-- <%}
    else
    { %>
                                     <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title pull-right">Hakkeem</h4>
                                    <%} %>--%>
                                </div>
                                <div class="modal-body">
                                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
    </section>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.4;
        }

        .modalPopup {
            /*background-color: #FFFFFF;*/
            border-width: 0px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 3px;
            width: 500px;
            height: 300px;
        }
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <%--  <%   if (Session["Language"].ToString() == "Auto")
          {%>--%>
    <link href="../Design/dist/css/skins/_all-skins.min.css" rel="stylesheet" />
  <%--  <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />--%>
   <%-- <%}
    else
    { %>
    <link href="arabicdesign/dist/css/skins/_all-skins.min.css" rel="stylesheet" />
  
    <%} %>--%>
  <%-- <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />--%>
</asp:Content>


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
public partial class Index_Mail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        String messagestr = "";
        string bg = "http://www.hakkeem.com/otp.png";
        messagestr = "<body style='background-color:#000000;background-repeat:no-repeat; background-image=url('" + bg+"')'><!--[if gte mso 9]><v:background xmlns:v='urn:schemas-microsoft-com:vml' fill='t'><v:fill type='tile' src='http://www.hakkeem.com/otp.png' color='#000000'/></v:background><![endif]--><table width='100%' border='2' cellspacing='0' cellpadding='0' bgcolor='red'  background='http://www.hakkeem.com/otp.png'><tr><td>body of the email here</td></tr></table></body>";
        //messagestr = "<body style='background-color:#000000;background-repeat:no-repeat; background-image=url('" + bg+"')'><!--[if gte mso 9]><v:background xmlns:v='urn:schemas-microsoft-com:vml' fill='t'><v:fill type='tile' src='http://www.hakkeem.com/otp.png' color='#000000'/></v:background><![endif]-->testing</body>";
        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add("ageorge@goldenetqan.com");
        mail.From = new MailAddress("mail@goldenetqan.com", "Hakkeem", System.Text.Encoding.UTF8);
        mail.Subject = "Hakkeem OTP";
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = messagestr.ToString();
        // mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Agrement.pdf"));
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;
        SmtpClient client = new SmtpClient();
        client.Credentials = new System.Net.NetworkCredential("mail@goldenetqan.com", "Etqan2016!!");
        client.Port = 587;
        client.Host = "smtp.goldenetqan.com";
        client.EnableSsl = false;
        try
        {
            client.Send(mail);

        }
        catch (Exception ex)
        {

        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        String messagestr = "";
        string bg = "http://www.hakkeem.com/otp.png";
        string follw = "http://www.hakkeem.com/followus.png";
        string face = "http://www.hakkeem.com/facebook-logo-button.png";
        string twitter = "http://www.hakkeem.com/twitter-logo-button.png";
        string insta = "http://www.hakkeem.com/instagram-logo.png";
        messagestr = "<body background='"+ bg + "'style='background-color:#e9e9e9;background-repeat: no-repeat;width:100%;background-position:center'>";
        messagestr = "<!--[if gte mso 9]><v:background xmlns:v='urn:schemas-microsoft-com:vml' fill='t'  src='" + bg + "'><v:fill type='tile' color='#e9e9e9'/><v:rect  fill='t' style='background-color:#e9e9e9;background-repeat:no-repeat;width:100%;background-position:center'></v:rect></v:background><![endif]-->";
        messagestr = "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='max-width:800px;margin:0 auto;padding:109px;font-family:Arial, Tahoma, Verdana;font-size:12px;line-height:1.6em;color:#7b7979;letter-spacing:0.02em;'>";
        //messagestr = "<!--[if gte mso 9]><v:background xmlns:v='urn:schemas-microsoft-com:vml' fill='t'><v:fill type='tile'/><v:rect  fill='t' style='max-width:800px;margin:0 auto;padding:109px;font-family:Arial, Tahoma, Verdana;font-size:12px;line-height:1.6em;color:#7b7979;letter-spacing:0.02em;'></v:rect></v:background><![endif]-->";
        messagestr = "<tbody><tr><td><tr><td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:105px;text-align:center'><tbody><tr>";
        messagestr = "<td width='100%' style='font-weight:bold;font-size:20px;color:#ffffff;font-family:sans-serif'>";
        messagestr = "OTP";
        //messagestr = "<!--[if gte mso 9]><v:shape><v:textbox id='mytextbox' inset='0,0,0,0'>";
        //messagestr = "<v:rect  fill='t' style='padding-top:105px;text-align:center;font-weight:bold;font-size:20px; color:#ffffff;font-family:sans-serif'>";
        //messagestr = "OTP</v:rect></v:textbox/></v:shape><![endif]-->";
        messagestr = "</tr></tbody></table></td></tr><tr><td>";
        messagestr = "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:120px;text-align:center'>";
        messagestr = "<tbody><tr><td width='100%' style='font-size:15px;color:#4aa9af;font-family: sans-serif'>";
        //messagestr = "OTP for registering in Hakkeem is: ######<!--[if gte mso 9]><v:shape><v:textbox id='mytextbox' inset='0,0,0,0'>";
        //messagestr = "<v:rect fill='t' style='padding-top:120px;text-align:center;font-size:15px;color:#4aa9af;font-family:sans-serif'>";
        //messagestr = "OTP for registering in Hakkeem is: ######</v:rect></v:textbox/></v:shape><![endif]-->";
        messagestr = "</tr></tbody></table></td></tr>";
        messagestr = "<tr><td><table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:75px'>";
        messagestr = "<tbody><tr><td style='text-align:left;'>";
        messagestr = "<span style='color:#4aa9af'><ahref='#'style='text-decoration:none;color:#4aa9af;font-size:11px'>Privacy policy</a>&nbsp;|&nbsp;<a href='#'style='text-decoration:none;color:#4aa9af;font-size:11px'>Contact Us</a>";
        messagestr = "</span></td><td style='text-align:right;'><img src='"+follw+"'> ";
        messagestr = "<!--[if gte mso 9]><v:background xmlns:v='urn:schemas-microsoft-com:vml' fill='t'><v:rect xmlns:v='urn:schemas-microsoft-com:vml' fill='true' stroke='false' style='width:20px;height:30px;'><v:fill type='tile' src='" + follw + "'/></v:background><![endif]>--> ";
        messagestr = "<a href='#' style='text-decoration:none'><img src='" + face + "' width='20px' height='20px' title='Facbook'>&nbsp;</a>";
        messagestr = " <!-- [if gte mso 9]><v:background xmlns:v='urn:schemas-microsoft-com:vml' fill='t'><v:rect xmlns:v='urn:schemas-microsoft-com:vml' fill='true' stroke='false' style='width:20px;height:30px;'><v:fill type='tile' src='" + face + "'/></v:background><![endif]>-->";
        messagestr = "<a href='#' style='text-decoration: none'><img src='"+twitter+"' width='20px' height='20px' title='Twitter'>&nbsp;</a>";
        messagestr = "<!--[if gte mso 9]><v:background xmlns:v='urn:schemas-microsoft-com:vml' fill='t'><v:rect xmlns:v='urn:schemas-microsoft-com:vml' fill='true' stroke='false' style='width:20px;height:30px;'><v:fill type='tile' src='" + twitter + "'/></v:background><![endif]>--> ";
        messagestr = "<a href='#' style='text-decoration: none'><img src='" + insta + "' width='20px' height='20px' title='Instagram'>&nbsp;</a>";
        messagestr = "<!--[if gte mso 9]><v:background xmlns:v='urn:schemas-microsoft-com:vml' fill='t'><v:rect xmlns:v='urn:schemas-microsoft-com:vml' fill='true' stroke='false' style='width:20px;height:30px;'><v:fill type='tile' src='" + insta + "'/></v:background><![endif]>--> ";
        messagestr = "</td></tr></tbody></table></td></tr>";
        messagestr = "<tr><td><table width ='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:25px'><tbody><tr><td width='100%' style='color:#4aa9af;font-family:sans-serif;font-size: 10px'><span style='font-weight:bold;font-size:11px'>Discliamer</span>:Please do not print this email unless it is necessary.Every unprinted email helps the environment.<!--[if gte mso 9]><v:shape><v:textbox id='mytextbox' inset='0,0,0,0'><v:rect fill='t' style='padding-top:25px;color:#4aa9af;font-family:sans-serif;font-size:10px'>Discliamer : Please do not print this email unless it is necessary.Every unprinted email helps the environment.</v:rect></v:textbox/></v:shape><![endif]--></td></tr></tbody></table></td></tr>";
        messagestr = "</td></tr></tbody></table></body>";
        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add("ageorge@goldenetqan.com");
        mail.From = new MailAddress("mail@goldenetqan.com", "Hakkeem", System.Text.Encoding.UTF8);
        mail.Subject = "Hakkeem OTP";
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = messagestr.ToString();
        // mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Agrement.pdf"));
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;
        SmtpClient client = new SmtpClient();
        client.Credentials = new System.Net.NetworkCredential("mail@goldenetqan.com", "Etqan2016!!");
        client.Port = 587;
        client.Host = "smtp.goldenetqan.com";
        client.EnableSsl = false;
        try
        {
            client.Send(mail);

        }
        catch (Exception ex)
        {

        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        String messagestr = "";
        string bg = "http://www.hakkeem.com/img/logo.png";
        string follw = "http://www.hakkeem.com/fu.png";
        string face = "http://www.hakkeem.com/f.png";
        string twitter = "http://www.hakkeem.com/t.png";
        string insta = "http://www.hakkeem.com/inst.png";
        messagestr = messagestr + "<body>";

        messagestr = messagestr + "<table align='center' width='800px' border='0' cellspacing='0' cellpadding='0' style='width:800px; margin:0 auto; background:#fff; font-family:Arial, Helvetica, sans-serif; font-size:12px; line-height:1.6em;color:#7b7979; letter-spacing:0.02em;'>";
        messagestr = messagestr + "<tbody>";
        messagestr = messagestr + "<tr>";
        messagestr = messagestr + " <td>";
        messagestr = messagestr + " <table width='100%' border='0' cellspacing='0'cellpadding='0' style='border-bottom:1px solid #000; padding-bottom:10px;font-family:Arial, Helvetica, sans-serif;'>";
        messagestr = messagestr + "<tbody>";
        messagestr = messagestr + " <tr>";
        messagestr = messagestr + "<td width='82%'><a href='http://www.qutof.com'><img src=" + "" + bg + "" + "></a></td>";
        //    messagestr = messagestr + "<td width='82%'><a href='http://www.qutof.com'><img src=" + "http://www.qutof.com/" + getEmbeddedImage(path) + "" + "></a></td>";

        messagestr = messagestr + "<td width='6%' align='right' valign='top'><a href='http://www.twitter.com'><img src='"+twitter+"'></a></td> ";
        messagestr = messagestr + "<td width='6%' align='right' valign='top'><a href='http://www.instagrm.com'><img src='"+insta+"'></a></td> ";
        messagestr = messagestr + "<td width='6%' align='right' valign='top'><a href='http://www.youtube.com'><img src='"+face+"'></a></td> ";
        messagestr = messagestr + " </tr>";
        messagestr = messagestr + "  </tr></tbody></table>";

        messagestr = messagestr + "<table width='100%' border='0'cellspacing='0' cellpadding='0' style='padding:20px 0;'>";
        messagestr = messagestr + "  </tr>";
        messagestr = messagestr + " <tr>";
        messagestr = messagestr + "<td style='text-transform:uppercase; font-size:18px; color:#000;font-family:Arial, Helvetica, sans-serif;'></td>";
        messagestr = messagestr + "<tr>";
        messagestr = messagestr + "<td style='padding:20px 0; color:#7b7979;font-family:Arial, Helvetica, sans-serif;'>Thank you for signing up with Hakkeem. We are delighted to have you with us and look forward to serving you with the best online shopping experience.<br>To be able to sign in to your account: <br><a href='http://www.qutof.com/user-signin.aspx' style='color:#0971ff; text-decoration:none;'>Click Here</a><br>Cheers!<br>Team Hakkeem</td></tr>";
        messagestr = messagestr + "<tr style='background:#4aa9af; color:#fff;'>";
        messagestr = messagestr + "<td colspan='2'>";
        messagestr = messagestr + "<table width='100%'><tr>";
        messagestr = messagestr + "<td width='49%' style='padding:20px;'>100% ORIGINAL</td>";
        messagestr = messagestr + " <td width='51%' align='right' style='padding:20px;font-family:Arial, Helvetica, sans-serif;'><a href='#' style='text-decoration:none; color:#fff;'></a></td>";
        messagestr = messagestr + "</tr></table></td>";
        messagestr = messagestr + " </tr>";
        messagestr = messagestr + "<tr>";
        messagestr = messagestr + " <td style='padding:20px 0 0; color:#7b7979;font-family:Arial, Helvetica, sans-serif;'>Question ? Please contact our customer service center<br> <strong style='color:#000;'>Send mail to, care@hakkeem.com </strong>";
        messagestr = messagestr + " Your registration is complete.<br />";
        messagestr = messagestr + "  </td>";
        messagestr = messagestr + "  </tr>";
        messagestr = messagestr + "</tbody>";
        messagestr = messagestr + "</table>";
        messagestr = messagestr + "</td>";
        messagestr = messagestr + "</tr>";
        messagestr = messagestr + "</tbody>";
        messagestr = messagestr + "</table>";
        messagestr = messagestr + "</body>";
        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        //mail.To.Add("ageorge@goldenetqan.com");
        mail.To.Add("ddavid@goldenetqan.com");
        mail.From = new MailAddress("mail@goldenetqan.com", "Hakkeem", System.Text.Encoding.UTF8);
        mail.Subject = "Hakkeem OTP";
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = messagestr.ToString();
        // mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Agrement.pdf"));
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;
        SmtpClient client = new SmtpClient();
        client.Credentials = new System.Net.NetworkCredential("mail@goldenetqan.com", "Etqan2016!!");
        client.Port = 587;
        client.Host = "smtp.goldenetqan.com";
        client.EnableSsl = false;
        try
        {
            client.Send(mail);

        }
        catch (Exception ex)
        {

        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {

        String messagestr = "";
        string bg = "http://www.hakkeem.com/head1.png";
        string follw = "http://www.hakkeem.com/followus.png";
        string face = "http://www.hakkeem.com/facebook.png";
        string twitter = "http://www.hakkeem.com/twitter.png";
        string insta = "http://www.hakkeem.com/instagram.png";
        string set = "http://www.hakkeem.com/setpng.png";
        messagestr = messagestr + "<body style='text-align:center;width=100%'>";

        messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='max-width:800px;margin:0 auto; background:#f2f2f2;padding:60px;font-family:Arial,Tahoma,Verdana;font-size:12px;line-height:1.6em;color:#7b7979;letter-spacing:0.02em;'>";
        messagestr = messagestr + "<tbody>";
        messagestr = messagestr + "<tr>";
        messagestr = messagestr + " <td>";
        messagestr = messagestr + " <table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding:20px 0;'>";
        messagestr = messagestr + "<tbody>";
        messagestr = messagestr + " <tr>";
        messagestr = messagestr + "<td  colspan='2'  style='padding:20px 20px;background-color:#fff;line-height:3.5em;font-size:12px;color:#4aa9af;text-align:center'><img src='" + bg + "' width='100%' ></td>";

        messagestr = messagestr + "</tr>";
        messagestr = messagestr + "<tr><td style='text-align:center;font-size:20px;padding:20px 20px;background-color: #fff;color:#4aa9af;font-weight:bold'>WELCOME TO HAKEEM</td></tr>";
        messagestr = messagestr + "<tr><td colspan='2' style='padding:20px 20px;background-color: #fff;line-height:2.2em;font-size:15px; color:#4aa9af;'>";
        messagestr = messagestr + "We're so happy you've joined Hakkeem family.<br><br> We founded Hakkeem because we wanted to create a trustworthy and inspiring platform for you to help arrange your timing and availability in an easy way,also to help you increasing your income.<br> Also we would like to Thank you for joining us and helping us to enchance the would.<br><strong> Here is your HakkeemID:HU_000115</strong><br> Your sincerely,<br> Hakkeem Team.";
        messagestr = messagestr + " </td></tr><tr><td style='text-align:center;padding:20px 20px;background-color: #fff;text-align:center'>";
        messagestr = messagestr + " <a href='#'><img src='"+set+"' height='35px'></a>";

        messagestr = messagestr + "</td></tr><tr><td  colspan='2' style='padding:20px 20px;background-color:#fff;'>";
        messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:10px'>";
        messagestr = messagestr + " <tbody><tr><td style='text-align:left;'>";
        messagestr = messagestr + "<span style='color:#4aa9af'><a href='#' style='text-decoration:none;color:#4aa9af;font-size:11px'>Privacy policy</a>&nbsp;|&nbsp;<a href='#' style='text-decoration:none;color:#4aa9af;font-size:11px'>Contact Us</a>";
        messagestr = messagestr + "</span></td><td style='text-align:right;'>";
        messagestr = messagestr + "<img src='" + follw + "'>";
        messagestr = messagestr + "<a href='#' style='text-decoration:none'><img src='" + face + "' title='Facbook'>&nbsp;</a><!--[if gte mso 9]><v:shape coordorigin = '0 0' coordsize ='100 100'></v:shape>";
        messagestr = messagestr + "<a href='#' style='text-decoration:none'><img src='" + twitter + "'  title='Twitter'>&nbsp;</a>";
        messagestr = messagestr + "<a href='#' style='text-decoration:none'><img src='" + insta + "'  title='Instagram'>&nbsp;</a>";
        messagestr = messagestr + "</td></tr></tbody></table></td></tr>";

        messagestr = messagestr + "<tr><td  colspan='2' style='background-color:#fff;padding:20px 20px'>";
        messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:25px'>";
        messagestr = messagestr + " <tbody><tr>";
        messagestr = messagestr + "<td width='100%' style='color:#4aa9af;font-family:sans-serif; font-size:10px'>";
        messagestr = messagestr + " <span style='font-weight:bold;font-size:10px'> Discliamer</span> : Please do not print this email unless it is necessary. Every unprinted email helps the environment.";
        messagestr = messagestr + "</td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></body>";
        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add("ageorge@goldenetqan.com");
         
        mail.From = new MailAddress("mail@goldenetqan.com", "Hakkeem", System.Text.Encoding.UTF8);
        mail.Subject = "Hakkeem OTP";
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = messagestr.ToString();
        // mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Agrement.pdf"));
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;
        SmtpClient client = new SmtpClient();
        client.Credentials = new System.Net.NetworkCredential("mail@goldenetqan.com", "Etqan2016!!");
        client.Port = 587;
        client.Host = "smtp.goldenetqan.com";
        client.EnableSsl = false;
        try
        {
            client.Send(mail);

        }
        catch (Exception ex)
        {

        }




    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        String messagestr = "";
        string bg = "http://www.hakkeem.com/head1.png";
        string follw = "http://www.hakkeem.com/followus.png";
        string face = "http://www.hakkeem.com/facebook.png";
        string twitter = "http://www.hakkeem.com/twitter.png";
        string insta = "http://www.hakkeem.com/instagram.png";
        string sthetho = "http://www.hakkeem.com/stethoscope1.png";
        string time = "http://www.hakkeem.com/time1.png";
        string calender = "http://www.hakkeem.com/calendar1.png";

        messagestr = messagestr + "<body  style='text-align:center;width=100%'>";

        messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='max-width:800px;margin:0 auto; background:#f2f2f2;padding:60px;font-family:Arial,Tahoma,Verdana;font-size:12px;line-height:1.6em;color:#7b7979;letter-spacing:0.02em;'>";
        messagestr = messagestr + "<tbody>";
        messagestr = messagestr + "<tr>";
        messagestr = messagestr + " <td>";
        messagestr = messagestr + " <table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding:20px 0;'>";
        messagestr = messagestr + "<tbody>";
        messagestr = messagestr + " <tr>";
        messagestr = messagestr + "<td  colspan='2'  style='padding:20px 20px;background-color:#fff;line-height:3.5em;font-size:12px;color:#4aa9af;text-align:center'><img src='" + bg + "' width='100%'></td>";

        messagestr = messagestr + "</tr>";
        messagestr = messagestr + "<tr><td colspan ='2' style='background-color:#fff;text-align:center;color:#4aa9af;font-size:20px;text-align:center;font-weight:bold;'>";
        messagestr = messagestr + "Appoinment</td></tr>";
        messagestr = messagestr + "<tr><td colspan ='2' style='background-color:#fff;text-align:center;color:#4aa9af;font-size:11px;line-height:1.5em;text-align:center;padding-top:10px'>";
        messagestr = messagestr + "'We believe that the greatest gift you can give your family and the world is a healthy you' </td></tr>";

        messagestr = messagestr + "<tr><td colspan='2' style='background-color:#fff;padding-top:15px'><table width='75%' border='0' cellspacing='0' cellpadding='0' style='padding:10px;border:1px solid #4aa9af;margin-left:85px'><tbody><tr><td  style='color:#4aa9af;font-weight:bold;'><img src='" + sthetho + "' ></td><td  style='font-size:15px;color:#4aa9af;text-align:right;font-weight:bold;font-family:san-serif'>Dr.Jacob</td></tr></tbody></table></td></tr>";
        messagestr = messagestr + "<tr><td colspan='2' style='background-color:#fff;padding-top:15px'><table width='75%' border='0' cellspacing='0' cellpadding='0' style='padding:10px;border:1px solid #4aa9af;margin-left:85px '><tbody><tr><td style='color:#4aa9af;font-weight:bold;'><img src='" + time + "'></td><td style='font-size:15px;color:#4aa9af;text-align:right;font-weight:bold;font-family:san-serif'>11:30 am</td></tr></tbody></table></td></tr>";
        messagestr = messagestr + "<tr><td colspan='2' style='background-color:#fff;padding-top:15px'><table width='75%' border='0' cellspacing='0' cellpadding='0' style='padding:10px;border:1px solid #4aa9af;margin-left:85px '><tbody><tr><td style='color:#4aa9af;font-weight:bold;'><img src='" + calender + "'></td><td  style='font-size:15px;color:#4aa9af;text-align:right;font-weight:bold;font-family:san-serif'>Dr.Jacob</td></tr></tbody></table></td></tr>";


        messagestr = messagestr + "<tr><td  colspan='2' style='padding:20px 20px;background-color:#fff;'>";
        messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:10px'>";
        messagestr = messagestr + " <tbody><tr><td style='text-align:left;'>";
        messagestr = messagestr + "<span style='color:#4aa9af'><a href='#' style='text-decoration:none;color:#4aa9af;font-size:11px'>Privacy policy</a>&nbsp;|&nbsp;<a href='#' style='text-decoration:none;color:#4aa9af;font-size:11px'>Contact Us</a>";
        messagestr = messagestr + "</span></td><td style='text-align:right;'>";
        messagestr = messagestr + "<img src='" + follw + "'>";
        messagestr = messagestr + "<a href='#' style='text-decoration:none'><img src='" + face + "' title='Facbook'>&nbsp;</a>";
        messagestr = messagestr + "<a href='#' style='text-decoration:none'><img src='" + twitter + "'  title='Twitter'>&nbsp;</a>";
        messagestr = messagestr + "<a href='#' style='text-decoration:none'><img src='" + insta + "'  title='Instagram'>&nbsp;</a>";
        messagestr = messagestr + "</td></tr></tbody></table></td></tr>";

        messagestr = messagestr + "<tr><td  colspan='2' style='background-color:#fff;padding:20px 20px'>";
        messagestr = messagestr + "<table width='100%' border='0' cellspacing='0' cellpadding='0' style='padding-top:25px'>";
        messagestr = messagestr + " <tbody><tr>";
        messagestr = messagestr + "<td width='100%' style='color:#4aa9af;font-family:sans-serif; font-size:10px'>";
        messagestr = messagestr + " <span style='font-weight:bold;font-size:10px'> Discliamer</span> : Please do not print this email unless it is necessary. Every unprinted email helps the environment.";
        messagestr = messagestr + "</td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></body>";
        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add("ageorge@goldenetqan.com");
       //  mail.To.Add("anugeorgekoodal@gmail.com");
        mail.From = new MailAddress("mail@goldenetqan.com", "Hakkeem", System.Text.Encoding.UTF8);
        mail.Subject = "Hakkeem APPOINMENT";
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = messagestr.ToString();
        // mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Agrement.pdf"));
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;
        SmtpClient client = new SmtpClient();
        client.Credentials = new System.Net.NetworkCredential("mail@goldenetqan.com", "Etqan2016!!");
        client.Port = 587;
        client.Host = "smtp.goldenetqan.com";
        client.EnableSsl = false;
        try
        {
            client.Send(mail);

        }
        catch (Exception ex)
        {

        }

    }
}
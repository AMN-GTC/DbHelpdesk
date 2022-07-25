using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using Helpdesk.Core.Common.Mailer;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Helpdesk.Core;
using MimeKit;
using MimeKit.Text;
using Helpdesk.Core.Entities;

namespace Helpdesk.Infrastructure.Common.Mailer
{
    public class MailerService : IMailerService
    {
        protected IHelpdeskUnitOfWork _emailunitOfWork;
        private readonly IOptions<MailConfig> _config;

        public MailerService(IOptions<MailConfig> config,IHelpdeskUnitOfWork emailUnitOfWork)
        {
            _config = config;
            _emailunitOfWork = emailUnitOfWork;
        }

        public async Task<List<Email>> GetUnreadEmail(MailConfig mailConfig)
        {

                using (var client = new ImapClient())
                {
                    using (var cancel = new CancellationTokenSource())
                    {
                    client.CheckCertificateRevocation = false;
                    client.Connect(_config.Value.ImapServer, 993, true, cancel.Token);


                        client.AuthenticationMechanisms.Remove("XOAUTH");

                        client.Authenticate(mailConfig.ImapUsername, mailConfig.ImapPassword, cancel.Token);


                        var inbox = client.Inbox;
                        inbox.Open(FolderAccess.ReadWrite, cancel.Token);

                        var UnreadIds = client.Inbox.Search(SearchQuery.NotSeen);

                        List<Email> emails = new List<Email>();
                    
                    foreach( var uid in UnreadIds)
                    {
                        var message = client.Inbox.GetMessage(uid);
                        if(message.InReplyTo == null)
                        {
                            
                            var emailMessage = new Email
                            {
                                ProjectId = mailConfig.ProjectId,
                                MsgID = message.MessageId,
                                HtmlAsBody = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
                                Subject = message.Subject,
                                Body = message.TextBody,
                                MailDateTime = message.Date.LocalDateTime,

                                From = Convert.ToString(message.From),
                                To = Convert.ToString(message.To)

                                
                            };

                            mailConfig.SmtpUsername = emailMessage.To;
                            mailConfig.SmtpUsernameTo = emailMessage.From;

                            emails.Add(emailMessage);

                            /*Jika ingin mengubah status pesan*/
                            inbox.SetFlags(uid, MessageFlags.Seen, true);
                        }
                    }
                    await _emailunitOfWork.SaveChangesAsync();
                    return emails;

                   /* client.Disconnect(true, cancel.Token);*/
                }
                }
        }

        public async Task<bool> SendEmail(MailConfig mailConfig, Email email)
        {

            var emails = new MimeMessage();

                emails.From.Add(MailboxAddress.Parse(mailConfig.SmtpUsername));
                emails.To.Add(MailboxAddress.Parse(email.To));
                emails.Subject = "AMN Helpdesk - Tiket '" + email.Subject +"' Telah Diterima";
 
            emails.Body = new TextPart(TextFormat.Html) { 
                    Text = @"
<!DOCTYPE html>

<html lang='en' xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'>

<head>

  <meta charset='utf-8'>

  <meta name='viewport' content='width=device-width'>

  <meta http-equiv='X-UA-Compatible' content='IE=edge'>

  <meta name='x-apple-disable-message-reformatting'>

  <meta name='format-detection' content='telephone=no,address=no,email=no,date=no,url=no'>

  <meta name='color-scheme' content='light'>

  <meta name='supported-color-schemes' content='light'>

  <title></title>



  



  <style>



    :root {

      color-scheme: light;

      supported-color-schemes: light;

    }

    html,

    body {

      margin: 0 auto !important;

      padding: 0 !important;

      height: 100% !important;

      width: 100% !important;

      font-family: Helvetica, Arial, sans-serif;

      color: rgba(49, 53, 59, 0.96);

    }

    * {

      -ms-text-size-adjust: 100%;

      -webkit-text-size-adjust: 100%;

    }

    div[style*='margin: 16px 0'] {

      margin: 0 !important;

    }

    #MessageViewBody, #MessageWebViewDiv{

      width: 100% !important;

    }

    table,

    td {

      mso-table-lspace: 0pt !important;

      mso-table-rspace: 0pt !important;

    }

    table {

      border-spacing: 0 !important;

      border-collapse: collapse !important;

      table-layout: fixed !important;

      margin: 0 auto !important;

    }

    img {

      -ms-interpolation-mode:bicubic;

    }

    a {

      text-decoration: none;

    }

    a[x-apple-data-detectors],

    .unstyle-auto-detected-links a,

    .aBn {

      border-bottom: 0 !important;

      cursor: default !important;

      color: inherit !important;

      text-decoration: none !important;

      font-size: inherit !important;

      font-family: inherit !important;

      font-weight: inherit !important;

      line-height: inherit !important;

    }

    .im {

      color: inherit !important;

    }

    .a6S {

      display: none !important;

      opacity: 0.01 !important;

    }

    img.g-img + div {

      display: none !important;

    }

    @media only screen and (min-device-width: 320px) and (max-device-width: 374px) {

      u ~ div .email-container {

        min-width: 320px !important;

      }

    }

    @media only screen and (min-device-width: 375px) and (max-device-width: 413px) {

      u ~ div .email-container {

        min-width: 375px !important;

      }

    }

    @media only screen and (min-device-width: 414px) {

      u ~ div .email-container {

        min-width: 414px !important;

      }

    }



  </style>



  <style>

    @media screen and (max-width: 480px) {

      .stack-column,

      .stack-column-center {

        display: block !important;

        width: 100% !important;

        max-width: 100% !important;

        direction: ltr !important;

      }

      .stack-column-half {

        display: inline-block !important;

        width: 50% !important;

        max-width: 50% !important;

        direction: ltr !important;

      }

      .stack-column-center {

        text-align: center !important;

      }

      .center-on-narrow {

        text-align: center !important;

        display: block !important;

        margin-left: auto !important;

        margin-right: auto !important;

        float: none !important;

      }

      img.full-on-narrow {

        width: 100% !important;

        max-width: 100% !important;

      }

      table.center-on-narrow {

        display: inline-block !important;

      }

    }

  </style>

</head>

<body width='100%' style='margin: 0; padding: 0 !important; mso-line-height-rule: exactly; background-color: #F8F8F8;'>

<center role='article' aria-roledescription='email' lang='en' style='width: 100%; background-color: #F8F8F8;'>

  



  <div style='max-width: 600px; margin: 0 auto;' class='email-container'>

    



    

    <table role='presentation' cellspacing='0' cellpadding='0' border='0' width='100%' style='margin: auto;'>

      

      <tr>

        <td align='left' style='padding: 24px 20px; background-color: #ffffff; text-align: left;'>

          <img src='https://amn.co.id/amn/img/logo_amn.png' width='148' height='32' alt='AMN logo' style='width: 100%; max-width: 148px; background: #fff; display: block;' class='g-img'>

        </td>

      </tr>

      



      <tr>

        <td style='padding: 8px 20px 16px; background-color: #FFFFFF;'>
          <h3 style='margin: 0; font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 20px; line-height: 26px;'>
            Hai <a href = 'mailto:  '+ email.To + ','>"+ email.To + @",</a>
            <br>
            Tiket telah berhasil dibuat, silahkan di cek terlebih dahulu!

          </h3>

          <p style='margin: 4px 0 0; font-family: Helvetica, Arial, sans-serif; font-size: 14px; line-height: 21px;'>

            Berikut detail tiket anda:

          </p>

        </td>

      </tr>



      <tr>

        <td style='padding: 20px; background-color: #ffffff'>

          <table role='presentation' cellspacing='0' border='0' width='100%'>

            <tr>

              <td style='padding: 0 16px 16px 16px; background-color: #f3f4f5; border-radius: 8px;'>

                <table role='presentation' cellspacing='0' border='0' width='100%'>

                  <tr>

                    <td>

                      <div class='stack-column' style='padding-top: 16px; display: inline-block; width: 100%; max-width: 260px; vertical-align: top;'>

                        <table role='presentation' cellspacing='0' border='0' width='100%'>

                          <tr>

                            <td style='padding-bottom: 16px;'>

                              <p style='margin: 0 0 2px; font-family: Helvetica, Arial, sans-serif; font-size: 12px; line-height: 18px; color: #727579;'>

                                ID Ticket

                              </p>

                              

                              <p style='margin: 0; font-family: Helvetica, Arial, sans-serif; font-size: 14px; font-weight: bold; line-height: 21px; color: #393d42;'>
                               
                                "+ email.TicketId + @"

                              </p>

                              





                            </td>

                          </tr>

                          <tr>

                            <td style='padding-bottom: 16px;'>

                              <p style='margin: 0 0 2px; font-family: Helvetica, Arial, sans-serif; font-size: 12px; line-height: 18px; color: #727579;'>

                                ID Project

                              </p>

                              <p style='margin: 0; font-family: Helvetica, Arial, sans-serif; font-size: 14px; font-weight: bold; line-height: 21px; color: #393d42;'>

                                

                                 " + email.ProjectId + @"

                                

                              </p>

                            </td>

                          </tr>



                          

                          <tr>

                            <td class='m-pb-16'>

                              <p style='margin: 0 0 2px; font-family: Helvetica, Arial, sans-serif; font-size: 12px; line-height: 18px; color: #727579;'>

                                Reported By

                              </p>

                              <p style='margin: 0; font-family: Helvetica, Arial, sans-serif; font-size: 14px; font-weight: bold; line-height: 21px; color: #393d42;'>

                                " + email.To + @"

                              </p>

                            </td>

                          </tr>

                          



                        </table>

                      </div>



                      <div class='stack-column' style='padding-top: 16px; display: inline-block; width: 100%; max-width: 260px; vertical-align: top;'>

                        <table role='presentation' cellspacing='0' border='0' width='100%'>

                          <tr>

                            <td style='padding-bottom: 16px;'>

                              <p style='margin: 0 0 2px; font-family: Helvetica, Arial, sans-serif; font-size: 12px; line-height: 18px; color: #727579;'>

                                Submission Date

                              </p>

                              <p style='margin: 0; font-family: Helvetica, Arial, sans-serif; font-size: 14px; font-weight: bold; line-height: 21px; color: #393d42;'>

                                " + email.MailDateTime + @"

                              </p>

                            </td>

                          </tr>

                          <tr>

                            <td style='padding-bottom: 16px;'>

                              <p style='margin: 0 0 2px; font-family: Helvetica, Arial, sans-serif; font-size: 12px; line-height: 18px; color: #727579;'>

                                Project Name

                              </p>

                              

                                

                                  <p style='margin: 0; font-family: Helvetica, Arial, sans-serif; font-size: 14px; font-weight: bold; line-height: 21px; color: #393d42;'>

                                    " + email.ProjectName + @"

                                  </p>

                                

                              

                            </td>

                          </tr>

                          <tr>

                            <td>

                              <p style='margin: 0 0 2px; font-family: Helvetica, Arial, sans-serif; font-size: 12px; line-height: 18px; color: #727579;'>

                                Ticket Status 

                              </p>

                              <p style='margin: 0; font-family: Helvetica, Arial, sans-serif; font-size: 14px; font-weight: bold; line-height: 21px; color: #393d42;'>

                                "+ email.StatusTicket + @"

                              </p>

                            </td>

                          </tr>

                        </table>

                      </div>

                    </td>

                  </tr>

                </table>

              </td>

            </tr>

          </table>

        </td>

      </tr>



      



      





      

      <tr>

        <td style='padding: 16px 20px; background-color: #FFFFFF;font-size: 12px;'>

          <div style='margin-bottom: 16px; font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 16px; line-height: 22px;'>

            Keterangan Tiket

          </div>



          <div style='border: 1px solid #F0F0F0; padding: 12px; border-radius: 8px;'>

            



            





            



            

            

            <div style='margin: 0; padding: 0;'>

              <p style='font-weight: normal; line-height: 1.6; margin: 0 0 20px; padding: 0;'>

                * Apabila terdapat ketidaksesuaian pada tiket tersebut, silahkan hubungi penanggung jawab projek anda.

              </p>

              <div style='margin: 0; padding: 0;'>

              <h3>Berikut ini merupakan keterangan dari tiket tersebut:</h3>

              
              " + email.Body + @"
            </div>
            

          </div>

        </td>

      </tr>

      



      

      <tr>

        <td style='padding: 16px 20px; background-color: #FFFFFF;'>

          <div style='margin-bottom: 16px; font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 16px; line-height: 22px;'>

            Konfirmasi status tiket? klik disini!

          </div>

          <a href='mailto:" + mailConfig.SmtpUsername + @"?subject=" + email.Subject+ @"&body=Dear AMN, Apakah tiket saya telah diproses?' style='background-color: #03AC0E; color: #FFFFFF; border-radius: 8px; height: 48px; line-height: 48px; display: inline-block; font-family: Helvetica, Arial, sans-serif; font-weight: bold; font-size: 16px; width: 100%; text-align: center; text-decoration: none;'>

            Konfirmasi Status Tiket

          </a>

        </td>

      </tr>

      



      

    

      



      

    </table>

    



    

    <table role='presentation' cellspacing='0' cellpadding='0' border='0' width='100%' style='margin: auto;'>

      <tr>

        <td style='padding: 24px 20px 0; background: #ffffff; font-family: sans-serif; font-size: 12px; line-height: 18px;'>

          <p style='margin: 0;'>E-mail ini dibuat otomatis, mohon tidak membalas. Jika butuh bantuan, silakan <a href='tel:0251 83 100 10 ' style='text-decoration: none; color: #03AC0E; font-weight: bold;'>hubungi AMN Care</a>.</p>

        </td>

      </tr>


      <tr>

        <td style='padding: 24px 20px 0; background: #ffffff;'>

          <table role='presentation' cellspacing='0' cellpadding='0' border='0' width='100%' style='border-top: 1px solid #E5E7E9;'>

            

            <tr>

              <td style='padding: 4px 0 24px; font-family: sans-serif; font-size: 12px; line-height: 18px; color: #bdbec0; text-align: center;'>

                <p style='margin: 0;'>2012-, PT AMN Indonesia</p>

              </td>

            </tr>

          </table>

        </td>

      </tr>

    </table>

    



    

  </div>



  

</center>

<p>&nbsp;<br></p>
<img src='https://fapp1.tokopedia.com/WSGQHPRTIW?id=16114=LRlUVgZSAwlXHhETRBgZQRMZRBIURRZGF0MQFUYYEkNCRURGF0QSGENCERNEGBlBEwkYVFUXUwNEAlhYB1xWIwUIBQ9bSlFXDh4ABVEPAVMHClIBAFwBUwNaAUkOTEYTEV9LSVEFQkhSTEVcD1dJBFdQBRxXCltJYDB3ZC5oYDcrMlsPUxhECw==' alt='' /></body>

</html>
"   

            }; 
                //format html 

                // sending email
                using var smtp = new SmtpClient();
                smtp.CheckCertificateRevocation = false;
                smtp.Connect(_config.Value.SmtpServer, _config.Value.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailConfig.SmtpUsername, mailConfig.SmtpPassword);
                await smtp.SendAsync(emails);
                smtp.Disconnect(true);
            
            await Task.FromResult(0);
            return true;
        }
    }
}

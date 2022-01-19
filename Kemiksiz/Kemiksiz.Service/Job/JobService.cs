using AutoMapper;
using Kemiksiz.DB.Entities.DataContext;
using Kemiksiz.Model;
using Kemiksiz.Model.User;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Service.Job
{
    public class JobService : IJobService
    {
        private readonly IMapper mapper;
        public JobService(IMapper _mapper)
        {
            mapper = _mapper;
        }

        public void sendWelcomeEmail()
        {
            var result = new General<UserViewModel>();
            using (var context = new KemiksizContext())
            {
                //Kullanci tablosundan Silinmemis ve Mail gönderilmemis olanları çekiyoruz.
                var userList = context.User.Where(x => !x.IsDelete && !x.IsSendEmail).OrderBy(x => x.Id);


                foreach (var user in userList)
                {
                    var message = new MimeMessage(); //Mailkit objesi
                    message.From.Add(new MailboxAddress("OSY", "osyapp@yandex.com")); //kimden gidecek
                    message.To.Add(new MailboxAddress("User", user.Email)); //kime gidecek
                    message.Subject = "Hoşgeldin"; //Mailin konusu
                    message.Body = new TextPart("html") //Mailin body si
                    {
                        Text = "Sayın " + user.Name + ", Online Site Yönetimine Hoşgeldiniz! <br>" + "Uygulama Şifreniz : " + user.Password
                    };

                    using (var client = new SmtpClient()) //smptp sağlayıcı
                    {
                        //client.Connect("smtp@gmail.com", 465, false);
                        //client.Connect("smtp@gmail.com", 587, SecureSocketOptions.StartTls);
                        client.Connect("smtp.yandex.com", 465, true); //port a bağlanıyoruz
                        client.Authenticate("talhaekrem0@yandex.com", "njfqmehnzooebuum");
                        //client.Send(message); //mesajı gönderiyoruz
                        client.Disconnect(true);
                    }

                    user.IsSendEmail = true;


                }
                context.SaveChanges();
            }

        }
    }
}

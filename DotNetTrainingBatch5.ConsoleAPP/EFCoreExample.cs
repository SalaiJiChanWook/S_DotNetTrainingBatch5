using DotNetTrainingBatch5.ConsoleAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.ConsoleAPP
{
    public class EFCoreExample
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var list = db.Blogs.Where(x=>x.DeleteFlag == false).ToList();
            foreach (var item in list) 
            {
                Console.WriteLine($"BlogId: {item.BlogId}");
                Console.WriteLine($"BlogTitle: {item.BlogTitle}");
                Console.WriteLine($"BlogAuthor: {item.BlogAuthor}");
                Console.WriteLine($"BlogContent: {item.BlogContent}");
            }
        }

        public void create( string title, string Author, string content)
        {
            BlogDataModel blog = new BlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = Author,
                BlogContent = content,
            };
            AppDbContext db = new AppDbContext();
            db.Blogs.Add( blog );
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Data are successfully Saved" : "Failed to save the data");
        }

        public void Edit(int id) { 
            AppDbContext db =new AppDbContext();
            //db.Blogs.Where(x => x.BlogId == id).FirstOrDefault();
            var item = db.Blogs.FirstOrDefault(x=> x.BlogId == id);
            if(item is null)
            {
                Console.WriteLine($"Thre is no data to show at ID :{id}");
                return;
            }

            Console.WriteLine($"BlogId:{item.BlogId}");
            Console.WriteLine($"BlogTitle:{item.BlogTitle}");
            Console.WriteLine($"BlogAutho:{item.BlogAuthor}");
            Console.WriteLine($"BlogConten:{item.BlogContent}");

        }

        public void update(int id, string title, string author, string content) {
            AppDbContext db = new AppDbContext();
            var iteam = db.Blogs
                .AsNoTracking()//အလုပ်မှာတော့သူ့ကိုသုံးပေးရတယ်။ ဒေတာဖတ်ရိုဘဲသွင်းရုံဘဲဆို performanceအတွက် အဆင်ပြေတယ်။ ဒါပေမယ့် အရေးကြီးတဲ့confidentialဒေတာတွေကိုင်တွယ်ရတဲ့အခါမှာတော့ AsNoTracking()ထက် Tracking()ကိုသုံးပေးရမယ်။
                               //Tracking()ကCache memoryထဲမှာ ဒေတာတွေကိုင်တွယ်တဲ့မှတ်တမ်းကို logတွေနှင့်သိမ်းထားပေးတယ့်အတွက်ကြောင့် ‌ေု‌ေ‌ေ‌ေ‌ေ‌ေဒေတာတွေပျောက်တာပျက်တာနှင့်ပြင်တာတွေကို ပြန်ရှာ/ပြန်ကြည့်နိုင်တယ်။
                               // AsNoTrackingက ၁၀မီလီစက္ကန့်နှင့်ပြီးနေချိန် Trackingက ၁၃မီလီစက္ကန့်မှပြီးတယ်
                .FirstOrDefault(x=>x.BlogId == id);
            if (iteam is null) {
                Console.WriteLine($"There are no data to update at Id:{id}");
                return;
            }
            if (!string.IsNullOrEmpty(title)) {
                iteam.BlogTitle = title;
            }
            if (!string.IsNullOrEmpty(author))
            {
                iteam.BlogAuthor = author;
            }

            if (!string.IsNullOrEmpty(content))
            {
                iteam.BlogContent = content;
            }

            db.Entry(iteam).State = EntityState.Modified;
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Data were saved successfully" : "Saving data was failed");
        }

        public void delete(int id) {
        
            AppDbContext db = new AppDbContext();
            var iteam = db.Blogs
               .AsNoTracking()//အလုပ်မှာတော့သူ့ကိုသုံးပေးရတယ်။
               .FirstOrDefault(x => x.BlogId == id);
            if (iteam is null)
            {
                Console.WriteLine($"There are no data to update at Id:{id}");
                return;
            }


            db.Entry(iteam).State = EntityState.Deleted;
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Data was deleted successfully" : "Data cannot be deleted");
        }
    }
}

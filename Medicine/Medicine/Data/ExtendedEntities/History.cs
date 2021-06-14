using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine.Data.Entities
{
    public partial class History
    {
        public Template TemplateExt
        {
            get
            {
                if (this.TargetId.HasValue)
                {
                    var target = App.Context.Targets.Find(this.TargetId);
                    App.Context.Entry(target).Reference(x => x.Template).Load();
                    return target?.Template;
                }
                else
                {
                    var resultData = App.Context.ResultDatas
                        .Where(x => x.HistoryId == this.Id)
                        .FirstOrDefault();

                    if (resultData != null)
                    {
                        App.Context.Entry(resultData).Reference(x => x.Item).Load();
                        App.Context.Entry(resultData.Item).Reference(x => x.Template).Load();
                        return resultData?.Item?.Template;
                    }
                }

                return null;
            }
        }

        public string TemplateName => this.TemplateExt?.Name;


    }
}

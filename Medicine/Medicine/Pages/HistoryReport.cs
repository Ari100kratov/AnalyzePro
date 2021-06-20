using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using Medicine.Data.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace Medicine.Pages
{
    public partial class HistoryReport : DevExpress.XtraReports.UI.XtraReport
    {
        public HistoryReport()
        {
            InitializeComponent();
        }

        public static XtraReport CreateReport(History history)
        {
            var report = new HistoryReport();
            report.ShowPrintStatusDialog = true;

            var patient = App.Context.Patients.Find(history.PatientId);
            report.photo.ImageSource = patient is null ? null : ByteToImage(patient.PhotoExt);
            report.LastName.Text = patient?.LastName ?? "-";
            report.FIrstName.Text = patient?.FirstName ?? "-";
            report.MiddleName.Text = patient?.MiddleName ?? "-";
            report.Age.Text = patient is null ? "-" : patient.Age + " лет";
            report.Gender.Text = patient?.Gender ?? "-";

            var template = App.Context.Templates.Find(history?.TemplateExt?.Id);
            report.Template.Text = template?.Name ?? "-";
            report.CreateDate.Text = history.CreateDate.Date.ToString();

            var targetList = new List<Target>();

            if (history.TargetId.HasValue)
                FillTargetList(history.TargetId.Value);

            if (targetList.Count == 0)
                report.TargetGroup.Text = "-";
            else
            {
                targetList.Reverse();
                var targetText = string.Join(" > ", targetList.Select(x => x.Name));
                report.TargetGroup.Text = targetText;
            }

            var results = App.Context.ResultDatas.Where(x => x.HistoryId == history.Id).ToList();
            report.table.BeginInit();

            foreach (var result in results)
            {
                var item = App.Context.Items.Find(result.ItemId);
                if (item is null)
                    continue;


                var row = new XRTableRow();
                row.BackColor = Color.Transparent;
                row.Parent = report.table;
                row.HeightF = 60;
                report.table.Rows.Add(row);

                row.Cells.Add(new XRTableCell
                {
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                    Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left,
                    Padding = 3,
                    Text = item.Name
                });

                string valueText;
                string resultText;
                if (item.TypeId == 0)
                {
                    if (!result.Value.HasValue)
                    {
                        valueText = "Не заполнено";
                        resultText = "Не установлено";
                    }
                    else
                    {
                        valueText = $"{result.Value.Value} {item.MeasureUnit}";

                        var border = App.Context.Borders.FirstOrDefault(x => x.ItemId == item.Id && x.TargetId == history.TargetId);
                        if (border is null)
                        {
                            resultText = "Пределы не заданы";
                        }
                        else
                        {
                            if (result.Value >= border.NormalMin && result.Value <= border.NormalMax)
                            {
                                row.BackColor = Color.LightGreen;
                                resultText = "В пределах нормы";
                            }
                            else if (result.Value >= border.WarningMin && result.Value <= border.WarningMax)
                            {
                                row.BackColor = Color.LightGoldenrodYellow;
                                resultText = "В пределах допустимого";
                            }
                            else
                            {
                                row.BackColor = Color.LightCoral;
                                resultText = "За пределами допустимого";
                            }
                        }
                    }
                }
                else
                {
                    var checkLists = App.Context.CheckLists.Where(x => x.ItemId == item.Id).ToList();
                    var checkList = checkLists.Find(x => x.Id == result.Value);

                    if (checkList is null)
                    {
                        valueText = "Не заполнено";
                        resultText = "Не установлено";
                    }
                    else
                    {

                        valueText = checkList.Name;

                        var border = App.Context.Borders.FirstOrDefault(x => x.ItemId == item.Id && x.TargetId == history.TargetId);
                        if (border is null)
                        {
                            resultText = "Пределы не заданы";
                        }
                        else
                        {
                            if (result.Value == border.NormalItem)
                            {
                                row.BackColor = Color.LightGreen;
                                resultText = "В пределах нормы";
                            }
                            else if (result.Value == border.WarningItem)
                            {
                                row.BackColor = Color.LightGoldenrodYellow;
                                resultText = "В пределах допустимого";
                            }
                            else
                            {
                                row.BackColor = Color.LightCoral;
                                resultText = "За пределами допустимого";
                            }
                        }
                    }
                }

                row.Cells.Add(new XRTableCell
                {
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                    Borders = DevExpress.XtraPrinting.BorderSide.Bottom,
                    Padding = 3,
                    Text = valueText
                });

                row.Cells.Add(new XRTableCell
                {
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                    Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Right,
                    Padding = 3,
                    Text = resultText
                });
            }

            report.table.AdjustSize();
            report.table.EndInit();

            void FillTargetList(int targetId)
            {
                var target = App.Context.Targets.Find(targetId);

                if (target != null)
                    targetList.Add(target);

                if (target.ParentId.HasValue)
                    FillTargetList(target.ParentId.Value);
            }



            return report;
        }


        public static ImageSource ByteToImage(byte[] imageData)
        {
            using (var ms = new MemoryStream(imageData))
            {
                return new ImageSource(Image.FromStream(ms));
            }
        }
    }
}

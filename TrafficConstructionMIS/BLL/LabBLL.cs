using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbModel;
using IDAL;
using IBLL;
using Newtonsoft.Json;
using MyTool;
using System.Linq.Expressions;
using MyModel;
using DAL;
//引用word对象类库和命名空间
using MSWord = Microsoft.Office.Interop.Word;
using System.IO;
using System.Reflection;
//引用drawing
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BLL
{

    public class LabBLL:BaseBLL,ILabBLL
    {
        ITb_Lab_TestDAL Tb_Lab_TestDAL { get; set; }
        ITb_Lab_TestItemDAL Tb_Lab_TestItemDAL { get; set; }
        ITb_Lab_DepartmentDAL Tb_Lab_DepartmentDAL { get; set; }
        ITb_Lab_ProjectNameDAL Tb_Lab_ProjectNameDAL { get; set; }
        ITb_Lab_CurveDAL Tb_Lab_CurveDAL { get; set; }
        ITb_Lab_TypeDAL Tb_Lab_TypeDAL { get; set; }
        //ILab_PrintChart Lab_PrintChart;
        

        public string GetTableData(int pageIndex, int pageSize, int deparmentId)
        {
            int total = 0;
            JqGridTable<Lab_MainTable> jqGridTable = new JqGridTable<Lab_MainTable>();
            List<Lab_MainTable> rows = new List<Lab_MainTable>();
            List<Tb_Lab_Test> pieces = Tb_Lab_TestDAL.LoadPageEntities<DateTime>(pageIndex, pageSize, out total, r =>r.DepartmentId==deparmentId, r => (DateTime)r.TestTime, false).ToList();

            foreach (Tb_Lab_Test piece in pieces)
            {
                Lab_MainTable table = new Lab_MainTable();
                table.Id = piece.Id;
                table.DepartmentName = piece.Tb_Lab_Department.DepartmentName;
                table.ProjectName = piece.Tb_Lab_ProjectName.ProjectName;
                table.TestNo = piece.TestNo;
                table.TestUser = piece.TestUser;
                table.TestDevice = piece.TestDevice;
                table.ReworkCount = piece.ReworkCount;
                table.TestTime = piece.TestTime.ToString();

                rows.Add(table);
            }
            jqGridTable.page = pageIndex;
            jqGridTable.records = total;
            jqGridTable.rows = rows;
            jqGridTable.total = total / pageSize;
            if (total % pageSize != 0) jqGridTable.total++;
            string json = JsonConvert.SerializeObject(jqGridTable);
            return json;
        }

        public string GetSelectOption()
        {

            List<Tb_Lab_Department> rows1 = new List<Tb_Lab_Department>();
            List<Tb_Lab_Department> pieces1 = Tb_Lab_DepartmentDAL.LoadEntities(r => true).ToList();
            foreach (Tb_Lab_Department piece in pieces1)
            {
                Tb_Lab_Department table = new Tb_Lab_Department();
                table.DepartmentName = piece.DepartmentName;
                table.Id = piece.Id;
                rows1.Add(table);
            }

            List<Tb_Lab_ProjectName> rows2 = new List<Tb_Lab_ProjectName>();
            List<Tb_Lab_ProjectName> pieces2 = Tb_Lab_ProjectNameDAL.LoadEntities(r => true).ToList();
            foreach (Tb_Lab_ProjectName piece in pieces2)
            {
                Tb_Lab_ProjectName table = new Tb_Lab_ProjectName();
                table.ProjectName = piece.ProjectName;
                table.Id = piece.Id;
                rows2.Add(table);
            }
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("DepartmentNames", rows1);
            dict.Add("ProjectNames", rows2);

            string json = JsonConvert.SerializeObject(dict);
            return json;
        }

        public string GetTableSearchData(int pageIndex, int pageSize, int departmentId, int testItemId, string testNo, string startTime, string endTime)
        {
            int total = 0;
            JqGridTable<Lab_MainTable> jqGridTable = new JqGridTable<Lab_MainTable>();
            List<Lab_MainTable> rows = new List<Lab_MainTable>();

            Expression<Func<Tb_Lab_Test, bool>> pieceWhere=(r=>true);
            if (!"".Equals(startTime) && startTime != null)
            {
                DateTime timStart = Convert.ToDateTime(startTime);
                pieceWhere = PredicateExtensions.And(pieceWhere, r => r.TestTime >= timStart);
            }
            if (!"".Equals(endTime) && endTime != null)
            {
                DateTime timeEnd = Convert.ToDateTime(endTime);
                pieceWhere = PredicateExtensions.And(pieceWhere, r => r.TestTime <= timeEnd);
            }
            if (departmentId != -1) pieceWhere = PredicateExtensions.And(pieceWhere, r => r.DepartmentId == departmentId);
            else pieceWhere = PredicateExtensions.And(pieceWhere, r => true);
            if (testItemId!=-1) pieceWhere = PredicateExtensions.And(pieceWhere, r => r.TestItemId == testItemId);
            else pieceWhere = PredicateExtensions.And(pieceWhere, r => true);
            if (!"".Equals(testNo) && testNo != null) pieceWhere = PredicateExtensions.And(pieceWhere, r => r.TestNo == testNo);
            List<Tb_Lab_Test> pieces = Tb_Lab_TestDAL.LoadPageEntities<DateTime>(pageIndex, pageSize, out total, pieceWhere, 
                r => (DateTime)r.TestTime, false).ToList();
            foreach (Tb_Lab_Test piece in pieces)
            {
                Lab_MainTable table = new Lab_MainTable();
                table.Id = piece.Id;
                table.DepartmentName = piece.Tb_Lab_Department.DepartmentName;
                table.ProjectName = piece.Tb_Lab_ProjectName.ProjectName;
                table.TestNo = piece.TestNo;
                table.TestUser = piece.TestUser;
                table.TestDevice = piece.TestDevice;
                table.ReworkCount = piece.ReworkCount;
                table.TestTime = piece.TestTime.ToString();

                
                rows.Add(table);
            }
            jqGridTable.page = pageIndex;
            jqGridTable.records = total;
            jqGridTable.rows = rows;
            jqGridTable.total = total / pageSize;
            if (total % pageSize != 0) jqGridTable.total++;
            string json = JsonConvert.SerializeObject(jqGridTable);
            return json;
        }

        public void GetDetailData(int Id, List<Tb_Lab_TestItem> labTestItem,List<Tb_Lab_Test> labTest)
        {
            labTestItem.AddRange(Tb_Lab_TestItemDAL.LoadEntities(r => r.TestId == Id).ToList());
            labTest.AddRange(Tb_Lab_TestDAL.LoadEntities(r=>r.Id==Id).ToList());
        }


        /// <summary>
        /// 潘承瑞，共四类曲线，1-4分别为钢筋拉伸（3条）、水泥抗折（3条）、水泥抗压（6条）、混凝土抗压（3条）
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lab_CurveChart</returns>
        public string GetCurveData(int id)
        {
            Lab_CurveChart curve = new Lab_CurveChart();
            List<Tb_Lab_Curve> data = Tb_Lab_CurveDAL.LoadEntities(r => r.TestId == id).ToList();
            if (data.Count != 0)
            {
                curve.curveNum = data.Count;
                int i=curve.curveNum;
                int curveType=(int)data[0].TypeId;

                //第一类曲线：钢筋拉伸
                if (curveType == 1) {
                    curve.Lab_CurveType = 1;
                    foreach (Tb_Lab_Curve type in data)   //type为id为指定值的某一条数据
                    {
                        List<Lab_QuXian_1> jsonData = JsonConvert.DeserializeObject<List<Lab_QuXian_1>>(type.Datas);   //将Datas字段反序列化，转化为List<Lab_QuXian>
                        if (curve.curve1Flag==0)
                        {
                            curve.curve1Flag = 1;
                            foreach (Lab_QuXian_1 point in jsonData)
                            {
                                curve.DateTime1.Add(point.Time);
                                curve.Force1.Add(point.Force);
                            }
                            i--;
                            continue;
                        }

                        if (curve.curve2Flag == 0&&i>0)
                        {
                            curve.curve2Flag = 1;
                            foreach (Lab_QuXian_1 point in jsonData)
                            {
                                curve.DateTime2.Add(point.Time);
                                curve.Force2.Add(point.Force);
                            }
                            i--;
                            continue;
                        }

                        if (curve.curve3Flag == 0&&i>0)
                        {
                            curve.curve3Flag = 1;
                            foreach (Lab_QuXian_1 point in jsonData)
                            {
                                curve.DateTime3.Add(point.Time);
                                curve.Force3.Add(point.Force);
                            }
                            i--;
                            continue;
                        }
                    }
                }


                //第二类曲线：水泥抗折
                if (curveType == 2)
                {
                    curve.Lab_CurveType = 2;
                    foreach (Tb_Lab_Curve type in data)   //type为id为指定值的某一条数据
                    {
                        List<Lab_QuXian_2> jsonData = JsonConvert.DeserializeObject<List<Lab_QuXian_2>>(type.Datas);   //将Datas字段反序列化，转化为List<Lab_QuXian>
                        //jsonData = jsonData.OrderBy(r => double.Parse(r.Time)).ToList();//按时间将数据进行排序
                        if (curve.curve1Flag == 0)
                        {
                            curve.curve1Flag = 1;
                            foreach (Lab_QuXian_2 point in jsonData)
                            {
                                curve.DateTime1.Add(point.Time);
                                curve.Force1.Add(point.Force);
                            }
                            i--;
                            continue;
                        }

                        if (curve.curve2Flag == 0 && i > 0)
                        {
                            curve.curve2Flag = 1;
                            foreach (Lab_QuXian_2 point in jsonData)
                            {
                                curve.DateTime2.Add(point.Time);
                                curve.Force2.Add(point.Force);
                            }
                            i--;
                            continue;
                        }

                        if (curve.curve3Flag == 0 && i > 0)
                        {
                            curve.curve3Flag = 1;
                            foreach (Lab_QuXian_2 point in jsonData)
                            {
                                curve.DateTime3.Add(point.Time);
                                curve.Force3.Add(point.Force);
                            }
                            i--;
                            continue;
                        }
                    }
                }


                //第三类曲线：水泥抗压
                if (curveType == 3)
                {
                    curve.Lab_CurveType = 3;
                    //int last_datalength = 0;//去除多余项
                    foreach (Tb_Lab_Curve type in data)   //type为id为指定值的某一条数据
                    {
                        List<Lab_QuXian_3> jsonData = JsonConvert.DeserializeObject<List<Lab_QuXian_3>>(type.Datas);   //将Datas字段反序列化，转化为List<Lab_QuXian>
                        
                        if (curve.curve1Flag == 0)
                        {
                            curve.curve1Flag = 1;
                            foreach (Lab_QuXian_3 point in jsonData)
                            {
                                curve.DateTime1.Add(point.Time);
                                curve.Force1.Add(point.Force);
                            }
                            i--;
                            //last_datalength = jsonData.Count;//去除多余项
                            continue;
                        }

                        if (curve.curve2Flag == 0 && i > 0)
                        {
                            curve.curve2Flag = 1;
                            //List<Lab_QuXian_3> jsonData_remove = jsonData.SkipWhile((n, index) => index < last_datalength).ToList();//去除多余项
                            foreach (Lab_QuXian_3 point in jsonData)
                            {
                                curve.DateTime2.Add(point.Time);
                                curve.Force2.Add(point.Force);
                            }
                            //foreach (Lab_QuXian_3 point in jsonData_remove)
                            //{
                            //    curve.DateTime2.Add(point.Time);
                            //    curve.Force2.Add(point.Force);
                            //}
                            i--;
                            //last_datalength = jsonData.Count;//去除多余项
                            continue;
                        }

                        if (curve.curve3Flag == 0 && i > 0)
                        {
                            curve.curve3Flag = 1;
                            //List<Lab_QuXian_3> jsonData_remove = jsonData.SkipWhile((n, index) => index < last_datalength).ToList();//去除多余项
                            foreach (Lab_QuXian_3 point in jsonData)
                            {
                                curve.DateTime3.Add(point.Time);
                                curve.Force3.Add(point.Force);
                            }
                            //foreach (Lab_QuXian_3 point in jsonData_remove)
                            //{
                            //    curve.DateTime3.Add(point.Time);
                            //    curve.Force3.Add(point.Force);
                            //}
                            i--;
                            //last_datalength = jsonData.Count;//去除多余项
                            continue;
                        }

                        if (curve.curve4Flag == 0 && i > 0)
                        {
                            curve.curve4Flag = 1;
                            //List<Lab_QuXian_3> jsonData_remove = jsonData.SkipWhile((n, index) => index < last_datalength).ToList();//去除多余项
                            foreach (Lab_QuXian_3 point in jsonData)
                            {
                                curve.DateTime4.Add(point.Time);
                                curve.Force4.Add(point.Force);
                            }
                            //foreach (Lab_QuXian_3 point in jsonData_remove)
                            //{
                            //    curve.DateTime4.Add(point.Time);
                            //    curve.Force4.Add(point.Force);
                            //}
                            i--;
                            //last_datalength = jsonData.Count;//去除多余项
                            continue;
                        }

                        if (curve.curve5Flag == 0 && i > 0)
                        {
                            curve.curve5Flag = 1;
                            //List<Lab_QuXian_3> jsonData_remove = jsonData.SkipWhile((n, index) => index < last_datalength).ToList();//去除多余项
                            foreach (Lab_QuXian_3 point in jsonData)
                            {
                                curve.DateTime5.Add(point.Time);
                                curve.Force5.Add(point.Force);
                            }
                            //foreach (Lab_QuXian_3 point in jsonData_remove)
                            //{
                            //    curve.DateTime5.Add(point.Time);
                            //    curve.Force5.Add(point.Force);
                            //}
                            i--;
                            //last_datalength = jsonData.Count;//去除多余项
                            continue;
                        }

                        if (curve.curve6Flag == 0 && i > 0)
                        {
                            curve.curve6Flag = 1;
                            //List<Lab_QuXian_3> jsonData_remove = jsonData.SkipWhile((n, index) => index < last_datalength).ToList();//去除多余项
                            foreach (Lab_QuXian_3 point in jsonData)
                            {
                                curve.DateTime6.Add(point.Time);
                                curve.Force6.Add(point.Force);
                            }
                            //foreach (Lab_QuXian_3 point in jsonData_remove)
                            //{
                            //    curve.DateTime6.Add(point.Time);
                            //    curve.Force6.Add(point.Force);
                            //}
                            i--;
                            //last_datalength = jsonData.Count;//去除多余项
                            continue;
                        }

                    }
                }


                //第四类曲线：混凝土抗压
                if (curveType == 4)
                {
                    curve.Lab_CurveType = 4;
                    foreach (Tb_Lab_Curve type in data)   //type为id为指定值的某一条数据
                    {
                        List<Lab_QuXian_4> jsonData = JsonConvert.DeserializeObject<List<Lab_QuXian_4>>(type.Datas);   //将Datas字段反序列化，转化为List<Lab_QuXian>
                        if (curve.curve1Flag == 0)
                        {
                            curve.curve1Flag = 1;
                            foreach (Lab_QuXian_4 point in jsonData)
                            {
                                curve.DateTime1.Add(point.Time);
                                curve.Force1.Add(point.Force);
                            }
                            i--;
                            continue;
                        }

                        if (curve.curve2Flag == 0 && i > 0)
                        {
                            curve.curve2Flag = 1;
                            foreach (Lab_QuXian_4 point in jsonData)
                            {
                                curve.DateTime2.Add(point.Time);
                                curve.Force2.Add(point.Force);
                            }
                            i--;
                            continue;
                        }

                        if (curve.curve3Flag == 0 && i > 0)
                        {
                            curve.curve3Flag = 1;
                            foreach (Lab_QuXian_4 point in jsonData)
                            {
                                curve.DateTime3.Add(point.Time);
                                curve.Force3.Add(point.Force);
                            }
                            i--;
                            continue;
                        }
                    }
                }


            }

            string json = JsonConvert.SerializeObject(curve);
            return json;
        }


        /// <summary>
        /// 潘承瑞，试验室打印
        /// </summary>
        /// <param name="Id"></param>
        public string Print(int Id)
        {
            List<Tb_Lab_Test> Lab_Test = Tb_Lab_TestDAL.LoadEntities(r => r.Id == Id).ToList();
            List<Tb_Lab_TestItem> Lab_TestItem = Tb_Lab_TestItemDAL.LoadEntities(r => r.TestId == Id).ToList();
            List<Tb_Lab_Curve> Lab_Curve = Tb_Lab_CurveDAL.LoadEntities(r => r.TestId == Id).ToList();
            int Lab_TestItem_Length=Lab_TestItem.Count;
            List<Lab_Print_Gjls> gjls = new List<Lab_Print_Gjls>();
            int curveNum = Lab_Curve.Count;
            string[] testDevice = Lab_Test[0].TestDevice.Split('-');
            string testType = testDevice[1];

            object path;                           //文件路径变量
            string title;                          //报表标题
            MSWord.Application wordApp;            //Word应用程序变量
            MSWord.Document wordDoc;              //Word文档变量

            path = @"E:/jtjsypt_data/YuHuan_TCMIS/lab/" + Lab_Test[0].TestDevice + '_' + Lab_Test[0].TestNo + "_"+Lab_Test[0].Id+ ".doc";    //保存路径
            string pathString=path.ToString();
            wordApp = new MSWord.ApplicationClass();  //初始化
            //wordApp.Visible = true;//使文档可见,调试文档格式时很有用

            //如果已存在，则删除
            if (File.Exists((string)path))
            {
                File.Delete((string)path);
            }
            //由于使用的是COM库，因此有许多变量需要用Missing.Value代替
            Object Nothing = Missing.Value;
            object unite = MSWord.WdUnits.wdStory;
            wordDoc = wordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            //WdSaveFormat为Word文档的保存格式,wdFormatDocument为Word 2007文档的保存格式。如果想创建docx文档,设置word格式时为object format =MSWord.WdSaveFormat.wdFormatDocumentDefault.
            object format = MSWord.WdSaveFormat.wdFormatDocument;

            //if (curveNum != 0)//如果curveNum为0，生成的word文件为空
            //{
            //第一类试验（钢筋拉伸）
            if (testType == "钢筋拉伸")
            {
                title = "钢筋拉伸 报表\n";    //这里换行符"\n"很重要,使后面添加的表格不会覆盖掉文本

                wordDoc.Paragraphs.Last.Range.Text = title;
                wordApp.Selection.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;//文本居中

                wordDoc.Paragraphs.First.Range.Font.Bold = 1;
                wordDoc.Paragraphs.First.Range.Font.Size = 18;
                wordApp.Selection.ParagraphFormat.LineSpacing = 16f;//设置文档的行间距
                wordDoc.Content.InsertAfter("\n");//这一句与下一句的顺序不能颠倒，原因还没搞透
                wordApp.Selection.EndKey(ref unite, ref Nothing); //将光标移动到文档末尾
                    
                //定义一个Word中的表格对象
                MSWord.Table table = wordDoc.Tables.Add(wordApp.Selection.Range, 9, 24, ref Nothing, ref Nothing);
                wordDoc.Content.InsertAfter("\n");//表格和曲线图片之间的空行
                //默认创建的表格没有边框，这里修改其属性，使得创建的表格带有边框
                table.Borders.Enable = 1;
                table.Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;//表格文本水平居中
                table.Range.Cells.VerticalAlignment = MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//表格文本垂直居中
                table.Rows.HeightRule = MSWord.WdRowHeightRule.wdRowHeightAtLeast;//高度规则是：行高有最低值下限？
                table.Rows.Height = wordApp.CentimetersToPoints(float.Parse("0.8"));// 

                wordApp.Selection.EndKey(ref unite, ref Nothing); //将光标移动到文档末尾

                //第一行
                table.Cell(1, 1).Merge(table.Cell(1, 4));//横向合并
                table.Cell(1, 2).Merge(table.Cell(1, 9));//横向合并
                table.Cell(1, 3).Merge(table.Cell(1, 6));//横向合并
                table.Cell(1, 4).Merge(table.Cell(1, 11));//横向合并

                //第二行
                table.Cell(2, 1).Merge(table.Cell(2, 4));//横向合并
                table.Cell(2, 2).Merge(table.Cell(2, 9));//横向合并
                table.Cell(2, 3).Merge(table.Cell(2, 6));//横向合并
                table.Cell(2, 4).Merge(table.Cell(2, 11));//横向合并

                //第三行
                table.Cell(3, 1).Merge(table.Cell(3, 4));//横向合并
                table.Cell(3, 2).Merge(table.Cell(3, 9));//横向合并
                table.Cell(3, 3).Merge(table.Cell(3, 8));//横向合并
                table.Cell(3, 4).Merge(table.Cell(3, 6));//横向合并
                table.Cell(3, 5).Merge(table.Cell(3, 7));//横向合并

                //第四行
                table.Cell(4, 1).Merge(table.Cell(4, 4));//横向合并
                table.Cell(4, 2).Merge(table.Cell(4, 9));//横向合并
                table.Cell(4, 3).Merge(table.Cell(4, 8));//横向合并
                table.Cell(4, 4).Merge(table.Cell(4, 6));//横向合并
                table.Cell(4, 5).Merge(table.Cell(4, 7));//横向合并

                //第五行
                table.Cell(5, 1).Merge(table.Cell(5, 4));//横向合并
                table.Cell(5, 2).Merge(table.Cell(5, 9));//横向合并
                table.Cell(5, 3).Merge(table.Cell(5, 8));//横向合并
                table.Cell(5, 4).Merge(table.Cell(5, 6));//横向合并
                table.Cell(5, 5).Merge(table.Cell(5, 7));//横向合并

                //第六行
                table.Cell(6, 1).Merge(table.Cell(6, 4));//横向合并
                table.Cell(6, 2).Merge(table.Cell(6, 9));//横向合并
                table.Cell(6, 3).Merge(table.Cell(6, 8));//横向合并
                table.Cell(6, 4).Merge(table.Cell(6, 6));//横向合并
                table.Cell(6, 5).Merge(table.Cell(6, 7));//横向合并

                //第七行
                table.Cell(7, 1).Merge(table.Cell(7, 4));//横向合并
                table.Cell(7, 2).Merge(table.Cell(7, 21));//横向合并

                //第八行
                table.Cell(8, 1).Merge(table.Cell(8, 4));//横向合并
                table.Cell(8, 2).Merge(table.Cell(8, 9));//横向合并
                table.Cell(8, 3).Merge(table.Cell(8, 6));//横向合并
                table.Cell(8, 4).Merge(table.Cell(8, 11));//横向合并

                //第九行
                table.Cell(9, 1).Merge(table.Cell(9, 4));//横向合并
                table.Cell(9, 2).Merge(table.Cell(9, 9));//横向合并
                table.Cell(9, 3).Merge(table.Cell(9, 6));//横向合并
                table.Cell(9, 4).Merge(table.Cell(9, 11));//横向合并


                table.Cell(1, 1).Range.Text = "试验编号";
                table.Cell(1, 3).Range.Text = "试验日期";
                table.Cell(2, 1).Range.Text = "焊接方法";
                table.Cell(2, 3).Range.Text = "钢筋级别";
                table.Cell(3, 1).Range.Text = "试件编号";
                table.Cell(3, 2).Range.Text = "最大力（kN）";
                table.Cell(3, 3).Range.Text = "抗拉强度（MPa）";
                table.Cell(4, 1).Range.Text = "1";
                table.Cell(5, 1).Range.Text = "2";
                table.Cell(6, 1).Range.Text = "3";
                table.Cell(7, 1).Range.Text = "检测机构";
                table.Cell(8, 1).Range.Text = "负责人";
                table.Cell(8, 3).Range.Text = "审核";
                table.Cell(9, 1).Range.Text = "试验人";
                table.Cell(9, 3).Range.Text = "打印日期";

                //从数据库中获取对应字段的值
                table.Cell(1, 2).Range.Text = Lab_Test[0].TestNo;
                table.Cell(1, 4).Range.Text = Lab_Test[0].TestTime.ToString();
                table.Cell(7, 2).Range.Text = Lab_Test[0].TestDevice;
                table.Cell(9, 2).Range.Text = Lab_Test[0].TestUser;
                table.Cell(9, 4).Range.Text = DateTime.Now.ToString();

                for (int i = 0; i < Lab_TestItem_Length; i++)
                {
                    if (Lab_TestItem[i].ItemName == "最大力1(N)")
                    {
                        double result;
                        result = Convert.ToDouble(Lab_TestItem[i].ItemResult);
                        table.Cell(4, 2).Range.Text = Math.Round((result / 1000), 1).ToString();
                        break;
                    }
                }
                for (int i = 0; i < Lab_TestItem_Length; i++)
                {
                    if (Lab_TestItem[i].ItemName == "最大力2(N)")
                    {
                        double result;
                        result = Convert.ToDouble(Lab_TestItem[i].ItemResult);
                        table.Cell(5, 2).Range.Text = Math.Round((result / 1000), 1).ToString();
                        break;
                    }
                }
                for (int i = 0; i < Lab_TestItem_Length; i++)
                {
                    if (Lab_TestItem[i].ItemName == "最大力3(N)")
                    {
                        double result;
                        result = Convert.ToDouble(Lab_TestItem[i].ItemResult);
                        table.Cell(6, 2).Range.Text = Math.Round((result / 1000), 1).ToString();
                        break;
                    }
                }

                int j = 0;
                //获取三个抗拉强度,并将相应字段写入word中
                for (int i = 0; i < Lab_TestItem_Length; i++)
                {

                    if (Lab_TestItem[i].ItemName == "抗拉强度-强度(MPa)")
                    {
                        //gjls[j].KangLQD = Lab_TestItem[i].ItemResult;
                        Lab_Print_Gjls item = new Lab_Print_Gjls();
                        item.KangLQD = Lab_TestItem[i].ItemResult;
                        gjls.Add(item);
                        j++;
                    }
                }
                if (j == 3)
                {
                    table.Cell(4, 3).Range.Text = gjls[0].KangLQD;
                    table.Cell(5, 3).Range.Text = gjls[1].KangLQD;
                    table.Cell(6, 3).Range.Text = gjls[2].KangLQD;
                }
                if (j == 2)
                {
                    table.Cell(4, 3).Range.Text = gjls[0].KangLQD;
                    table.Cell(5, 3).Range.Text = gjls[1].KangLQD;
                }
                if (j == 1)
                {
                    table.Cell(4, 3).Range.Text = gjls[0].KangLQD;
                }

            }


            //第二类试验：水泥抗折   第三类曲线：水泥抗压
            if (testType == "水泥抗折" || testType == "水泥抗压")
            {
                title = "水泥抗折抗压 报表\n";    //这里换行符"\n"很重要,使表格不会覆盖掉文本

                wordDoc.Paragraphs.Last.Range.Text = title;
                wordApp.Selection.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;//文本居中

                wordDoc.Paragraphs.First.Range.Font.Bold = 1;
                wordDoc.Paragraphs.First.Range.Font.Size = 18;
                wordApp.Selection.ParagraphFormat.LineSpacing = 16f;//设置文档的行间距
                wordDoc.Content.InsertAfter("\n");//这一句与下一句的顺序不能颠倒，原因还没搞透
                wordApp.Selection.EndKey(ref unite, ref Nothing); //将光标移动到文档末尾
                    
                //定义一个Word中的表格对象
                MSWord.Table table = wordDoc.Tables.Add(wordApp.Selection.Range, 12, 24, ref Nothing, ref Nothing);
                wordDoc.Content.InsertAfter("\n");//表格和曲线图片之间的空行
                //默认创建的表格没有边框，这里修改其属性，使得创建的表格带有边框
                table.Borders.Enable = 1;
                table.Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;//表格文本水平居中
                table.Range.Cells.VerticalAlignment = MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//表格文本垂直居中
                table.Rows.HeightRule = MSWord.WdRowHeightRule.wdRowHeightAtLeast;//高度规则是：行高有最低值下限？
                table.Rows.Height = wordApp.CentimetersToPoints(float.Parse("0.8"));// 

                wordApp.Selection.EndKey(ref unite, ref Nothing); //将光标移动到文档末尾 

                //第一行
                table.Cell(1, 1).Merge(table.Cell(1, 4));//横向合并
                table.Cell(1, 2).Merge(table.Cell(1, 9));//横向合并
                table.Cell(1, 3).Merge(table.Cell(1, 6));//横向合并
                table.Cell(1, 4).Merge(table.Cell(1, 11));//横向合并

                //第二行
                table.Cell(2, 1).Merge(table.Cell(3, 4));//纵、横向合并
                table.Cell(2, 2).Merge(table.Cell(2, 11));//横向合并
                table.Cell(2, 3).Merge(table.Cell(2, 12));//横向合并

                //第三行
                table.Cell(3, 2).Merge(table.Cell(3, 4));//横向合并
                table.Cell(3, 3).Merge(table.Cell(3, 5));//横向合并
                table.Cell(3, 4).Merge(table.Cell(3, 7));//横向合并
                table.Cell(3, 5).Merge(table.Cell(3, 7));//横向合并
                table.Cell(3, 6).Merge(table.Cell(3, 8));//横向合并
                table.Cell(3, 7).Merge(table.Cell(3, 10));//横向合并

                //第一列
                table.Cell(4, 1).Merge(table.Cell(9, 4));//纵、横向合并

                //第二列，4-9行
                table.Cell(4, 2).Merge(table.Cell(4, 4));//横向合并
                table.Cell(5, 2).Merge(table.Cell(5, 4));//横向合并
                table.Cell(6, 2).Merge(table.Cell(6, 4));//横向合并
                table.Cell(7, 2).Merge(table.Cell(7, 4));//横向合并
                table.Cell(8, 2).Merge(table.Cell(8, 4));//横向合并
                table.Cell(9, 2).Merge(table.Cell(9, 4));//横向合并

                //第三列
                table.Cell(4, 3).Merge(table.Cell(9, 5));//纵、横向合并

                //第四列
                table.Cell(4, 4).Merge(table.Cell(9, 7));//纵、横向合并

                //第五列，4-9行
                table.Cell(4, 5).Merge(table.Cell(4, 7));//横向合并
                table.Cell(5, 5).Merge(table.Cell(5, 7));//横向合并
                table.Cell(6, 5).Merge(table.Cell(6, 7));//横向合并
                table.Cell(7, 5).Merge(table.Cell(7, 7));//横向合并
                table.Cell(8, 5).Merge(table.Cell(8, 7));//横向合并
                table.Cell(9, 5).Merge(table.Cell(9, 7));//横向合并

                //第六列
                table.Cell(4, 6).Merge(table.Cell(9, 8));//纵、横向合并

                //第七列
                table.Cell(4, 7).Merge(table.Cell(9, 10));//纵、横向合并

                //第十行
                table.Cell(10, 1).Merge(table.Cell(10, 4));//横向合并
                table.Cell(10, 2).Merge(table.Cell(10, 21));//横向合并

                //第十一行
                table.Cell(11, 1).Merge(table.Cell(11, 4));//横向合并
                table.Cell(11, 2).Merge(table.Cell(11, 9));//横向合并
                table.Cell(11, 3).Merge(table.Cell(11, 6));//横向合并
                table.Cell(11, 4).Merge(table.Cell(11, 11));//横向合并

                //第十二行
                table.Cell(12, 1).Merge(table.Cell(12, 4));//横向合并
                table.Cell(12, 2).Merge(table.Cell(12, 9));//横向合并
                table.Cell(12, 3).Merge(table.Cell(12, 6));//横向合并
                table.Cell(12, 4).Merge(table.Cell(12, 11));//横向合并

                table.Cell(1, 1).Range.Text = "试验编号";
                table.Cell(1, 3).Range.Text = "试验日期";
                table.Cell(2, 1).Range.Text = "龄期";
                table.Cell(2, 2).Range.Text = "抗折";
                table.Cell(2, 3).Range.Text = "抗压";
                table.Cell(3, 2).Range.Text = "荷重(N)";
                table.Cell(3, 3).Range.Text = "平均荷重(N)";
                table.Cell(3, 4).Range.Text = "平均有效强度(MPa)";
                table.Cell(3, 5).Range.Text = "荷重(N)";
                table.Cell(3, 6).Range.Text = "平均荷重(KN)";
                table.Cell(3, 7).Range.Text = "平均有效强度(MPa)";
                table.Cell(10, 1).Range.Text = "检测机构";
                table.Cell(11, 1).Range.Text = "负责人";
                table.Cell(11, 3).Range.Text = "审核";
                table.Cell(12, 1).Range.Text = "试验人";
                table.Cell(12, 3).Range.Text = "打印日期";

                //设置字体大小
                table.Cell(3, 3).Range.Font.Size = 7F;
                table.Cell(3, 4).Range.Font.Size = 7F;
                table.Cell(3, 6).Range.Font.Size = 7F;
                table.Cell(3, 7).Range.Font.Size = 7F;

                table.Cell(4, 5).Range.Font.Size = 9F;
                table.Cell(5, 5).Range.Font.Size = 9F;
                table.Cell(6, 5).Range.Font.Size = 9F;
                table.Cell(7, 5).Range.Font.Size = 9F;
                table.Cell(8, 5).Range.Font.Size = 9F;
                table.Cell(9, 5).Range.Font.Size = 9F;

                table.Cell(3, 3).Range.Font.Bold = 1;//加粗
                table.Cell(3, 4).Range.Font.Bold = 1;//加粗
                table.Cell(3, 6).Range.Font.Bold = 1;//加粗
                table.Cell(3, 7).Range.Font.Bold = 1;//加粗


                //从数据库中获取对应字段的值
                table.Cell(1, 2).Range.Text = Lab_Test[0].TestNo;
                table.Cell(1, 4).Range.Text = Lab_Test[0].TestTime.ToString();
                table.Cell(10, 2).Range.Text = Lab_Test[0].TestDevice;
                table.Cell(12, 2).Range.Text = Lab_Test[0].TestUser;
                table.Cell(12, 4).Range.Text = DateTime.Now.ToString();

                if (testType == "水泥抗折")//第二类试验：水泥抗折
                {
                    for (int i = 0; i < Lab_TestItem_Length; i++)
                    {
                        if (Lab_TestItem[i].ItemName == "抗折荷重1(N)")
                        {
                            table.Cell(4, 2).Range.Text = Lab_TestItem[i].ItemResult;
                            break;
                        }
                    }
                    for (int i = 0; i < Lab_TestItem_Length; i++)
                    {
                        if (Lab_TestItem[i].ItemName == "抗折荷重2(N)")
                        {
                            table.Cell(5, 2).Range.Text = Lab_TestItem[i].ItemResult;
                            break;
                        }
                    }
                    for (int i = 0; i < Lab_TestItem_Length; i++)
                    {
                        if (Lab_TestItem[i].ItemName == "抗折荷重3(N)")
                        {
                            table.Cell(6, 2).Range.Text = Lab_TestItem[i].ItemResult;
                            break;
                        }
                    }
                    for (int i = 0; i < Lab_TestItem_Length; i++)
                    {
                        if (Lab_TestItem[i].ItemName == "抗折荷重(N)")
                        {
                            table.Cell(4, 3).Range.Text = Lab_TestItem[i].ItemResult;
                            break;
                        }
                    }
                    for (int i = 0; i < Lab_TestItem_Length; i++)
                    {
                        if (Lab_TestItem[i].ItemName == "抗折应力（MPa）")
                        {
                            table.Cell(4, 4).Range.Text = Lab_TestItem[i].ItemResult;
                            break;
                        }
                    }
                }
                if (testType == "水泥抗压")//第三类试验：水泥抗压
                {
                    for (int i = 0; i < Lab_TestItem_Length; i++)
                    {
                        if (Lab_TestItem[i].ItemName == "抗压荷重1(N)")
                        {
                            table.Cell(4, 5).Range.Text = Lab_TestItem[i].ItemResult;
                            break;
                        }
                    }
                    for (int i = 0; i < Lab_TestItem_Length; i++)
                    {
                        if (Lab_TestItem[i].ItemName == "抗压荷重2(N)")
                        {
                            table.Cell(5, 5).Range.Text = Lab_TestItem[i].ItemResult;
                            break;
                        }
                    }
                    for (int i = 0; i < Lab_TestItem_Length; i++)
                    {
                        if (Lab_TestItem[i].ItemName == "抗压荷重3(N)")
                        {
                            table.Cell(6, 5).Range.Text = Lab_TestItem[i].ItemResult;
                            break;
                        }
                    }
                    for (int i = 0; i < Lab_TestItem_Length; i++)
                    {
                        if (Lab_TestItem[i].ItemName == "抗压荷重4(N)")
                        {
                            table.Cell(7, 5).Range.Text = Lab_TestItem[i].ItemResult;
                            break;
                        }
                    }
                    for (int i = 0; i < Lab_TestItem_Length; i++)
                    {
                        if (Lab_TestItem[i].ItemName == "抗压荷重5(N)")
                        {
                            table.Cell(8, 5).Range.Text = Lab_TestItem[i].ItemResult;
                            break;
                        }
                    }
                    for (int i = 0; i < Lab_TestItem_Length; i++)
                    {
                        if (Lab_TestItem[i].ItemName == "抗压荷重6(N)")
                        {
                            table.Cell(9, 5).Range.Text = Lab_TestItem[i].ItemResult;
                            break;
                        }
                    }
                    for (int i = 0; i < Lab_TestItem_Length; i++)
                    {
                        if (Lab_TestItem[i].ItemName == "抗压荷重(KN)")
                        {
                            table.Cell(4, 6).Range.Text = Lab_TestItem[i].ItemResult;
                            break;
                        }
                    }
                    for (int i = 0; i < Lab_TestItem_Length; i++)
                    {
                        if (Lab_TestItem[i].ItemName == "抗压应力（MPa）")
                        {
                            table.Cell(4, 7).Range.Text = Lab_TestItem[i].ItemResult;
                            break;
                        }
                    }
                }


            }


            //第四类试验：混凝土抗压
            if (testType == "混凝土抗压")
            {
                title = "混凝土抗压 报表\n";    //这里换行符"\n"很重要,使表格不会覆盖掉文本

                wordDoc.Paragraphs.Last.Range.Text = title;
                wordApp.Selection.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;//文本居中

                wordDoc.Paragraphs.First.Range.Font.Bold = 1;
                wordDoc.Paragraphs.First.Range.Font.Size = 18;
                wordApp.Selection.ParagraphFormat.LineSpacing = 16f;//设置文档的行间距
                wordDoc.Content.InsertAfter("\n");//这一句与下一句的顺序不能颠倒，原因还没搞透
                wordApp.Selection.EndKey(ref unite, ref Nothing); //将光标移动到文档末尾
                    
                //定义一个Word中的表格对象
                MSWord.Table table = wordDoc.Tables.Add(wordApp.Selection.Range, 8, 24, ref Nothing, ref Nothing);
                wordDoc.Content.InsertAfter("\n");//表格和曲线图片之间的空行
                //默认创建的表格没有边框，这里修改其属性，使得创建的表格带有边框
                table.Borders.Enable = 1;
                table.Range.ParagraphFormat.Alignment = MSWord.WdParagraphAlignment.wdAlignParagraphCenter;//表格文本水平居中
                table.Range.Cells.VerticalAlignment = MSWord.WdCellVerticalAlignment.wdCellAlignVerticalCenter;//表格文本垂直居中
                table.Rows.HeightRule = MSWord.WdRowHeightRule.wdRowHeightAtLeast;//高度规则是：行高有最低值下限？
                table.Rows.Height = wordApp.CentimetersToPoints(float.Parse("0.8"));// 

                wordApp.Selection.EndKey(ref unite, ref Nothing); //将光标移动到文档末尾

                //第一行
                table.Cell(1, 1).Merge(table.Cell(1, 4));//横向合并
                table.Cell(1, 2).Merge(table.Cell(1, 9));//横向合并
                table.Cell(1, 3).Merge(table.Cell(1, 6));//横向合并
                table.Cell(1, 4).Merge(table.Cell(1, 11));//横向合并

                //第二行
                table.Cell(2, 1).Merge(table.Cell(2, 4));//横向合并
                table.Cell(2, 2).Merge(table.Cell(2, 9));//横向合并
                table.Cell(2, 3).Merge(table.Cell(2, 6));//横向合并
                table.Cell(2, 4).Merge(table.Cell(2, 11));//横向合并

                //第三行
                table.Cell(3, 1).Merge(table.Cell(3, 2));//横向合并
                table.Cell(3, 2).Merge(table.Cell(3, 5));//横向合并
                table.Cell(3, 3).Merge(table.Cell(3, 6));//横向合并
                table.Cell(3, 4).Merge(table.Cell(3, 7));//横向合并
                table.Cell(3, 5).Merge(table.Cell(3, 9));//横向合并
                table.Cell(3, 6).Merge(table.Cell(3, 10));//横向合并

                //第四行
                table.Cell(4, 1).Merge(table.Cell(5, 2));//纵、横向合并
                table.Cell(4, 2).Merge(table.Cell(4, 3));//横向合并
                table.Cell(4, 3).Merge(table.Cell(4, 4));//横向合并
                table.Cell(4, 4).Merge(table.Cell(4, 5));//横向合并
                table.Cell(4, 5).Merge(table.Cell(4, 6));//横向合并
                table.Cell(4, 6).Merge(table.Cell(4, 7));//横向合并
                table.Cell(4, 7).Merge(table.Cell(4, 8));//横向合并

                //第五行
                table.Cell(5, 2).Merge(table.Cell(5, 3));//横向合并
                table.Cell(5, 3).Merge(table.Cell(5, 4));//横向合并
                table.Cell(5, 4).Merge(table.Cell(5, 5));//横向合并
                table.Cell(5, 5).Merge(table.Cell(5, 6));//横向合并
                table.Cell(5, 6).Merge(table.Cell(5, 7));//横向合并
                table.Cell(5, 7).Merge(table.Cell(5, 8));//横向合并

                table.Cell(4, 8).Merge(table.Cell(5, 12));//纵、横向合并
                table.Cell(4, 9).Merge(table.Cell(5, 13));//纵、横向合并

                //第六行
                table.Cell(6, 1).Merge(table.Cell(6, 4));//横向合并
                table.Cell(6, 2).Merge(table.Cell(6, 21));//横向合并

                //第七行
                table.Cell(7, 1).Merge(table.Cell(7, 4));//横向合并
                table.Cell(7, 2).Merge(table.Cell(7, 9));//横向合并
                table.Cell(7, 3).Merge(table.Cell(7, 6));//横向合并
                table.Cell(7, 4).Merge(table.Cell(7, 11));//横向合并

                //第八行
                table.Cell(8, 1).Merge(table.Cell(8, 4));//横向合并
                table.Cell(8, 2).Merge(table.Cell(8, 9));//横向合并
                table.Cell(8, 3).Merge(table.Cell(8, 6));//横向合并
                table.Cell(8, 4).Merge(table.Cell(8, 11));//横向合并

                table.Cell(1, 1).Range.Text = "试验编号";
                table.Cell(1, 3).Range.Text = "试验日期";
                table.Cell(2, 1).Range.Text = "试件规格(mm*mm*mm)";
                table.Cell(2, 3).Range.Text = "强度等级";
                table.Cell(3, 1).Range.Text = "龄期";
                table.Cell(3, 2).Range.Text = "1";
                table.Cell(3, 3).Range.Text = "2";
                table.Cell(3, 4).Range.Text = "3";
                table.Cell(3, 5).Range.Text = "有效力值(kN)";
                table.Cell(3, 6).Range.Text = "有效强度(MPa)";
                table.Cell(4, 2).Range.Text = "力值";
                table.Cell(4, 3).Range.Text = "强度";
                table.Cell(4, 4).Range.Text = "力值";
                table.Cell(4, 5).Range.Text = "强度";
                table.Cell(4, 6).Range.Text = "力值";
                table.Cell(4, 7).Range.Text = "强度";
                table.Cell(6, 1).Range.Text = "检测机构";
                table.Cell(7, 1).Range.Text = "负责人";
                table.Cell(7, 3).Range.Text = "审核";
                table.Cell(8, 1).Range.Text = "试验人";
                table.Cell(8, 3).Range.Text = "打印日期";

                //设置字体大小
                table.Cell(2, 1).Range.Font.Size = 9F;
                table.Cell(5, 2).Range.Font.Size = 9F;
                table.Cell(5, 3).Range.Font.Size = 9F;
                table.Cell(5, 4).Range.Font.Size = 9F;
                table.Cell(5, 5).Range.Font.Size = 9F;
                table.Cell(5, 6).Range.Font.Size = 9F;
                table.Cell(5, 7).Range.Font.Size = 9F;

                //从数据库中获取对应字段的值
                table.Cell(1, 2).Range.Text = Lab_Test[0].TestNo;
                table.Cell(1, 4).Range.Text = Lab_Test[0].TestTime.ToString();
                table.Cell(6, 2).Range.Text = Lab_Test[0].TestDevice;
                table.Cell(8, 2).Range.Text = Lab_Test[0].TestUser;
                table.Cell(8, 4).Range.Text = DateTime.Now.ToString();
                for (int i = 0; i < Lab_TestItem_Length; i++)
                {
                    if (Lab_TestItem[i].ItemName == "试件规格（mm）")
                    {
                        table.Cell(2, 2).Range.Text = Lab_TestItem[i].ItemResult;
                        break;
                    }
                }

                for (int i = 0; i < Lab_TestItem_Length; i++)
                {
                    if (Lab_TestItem[i].ItemName == "强度等级")
                    {
                        table.Cell(2, 4).Range.Text = Lab_TestItem[i].ItemResult;
                        break;
                    }
                }

                for (int i = 0; i < Lab_TestItem_Length; i++)
                {
                    if (Lab_TestItem[i].ItemName == "试件龄期(天)")
                    {
                        table.Cell(4, 1).Range.Text = Lab_TestItem[i].ItemResult + "天";
                        break;
                    }
                }

                for (int i = 0; i < Lab_TestItem_Length; i++)
                {
                    if (Lab_TestItem[i].ItemName == "抗压荷重1(N)")
                    {
                        double result;
                        result = Convert.ToDouble(Lab_TestItem[i].ItemResult);
                        table.Cell(5, 2).Range.Text = Math.Round((result / 1000), 1).ToString();
                        break;
                    }
                }

                for (int i = 0; i < Lab_TestItem_Length; i++)
                {
                    if (Lab_TestItem[i].ItemName == "抗压强度1(MPa)")
                    {
                        table.Cell(5, 3).Range.Text = Math.Round(Convert.ToDouble(Lab_TestItem[i].ItemResult), 1).ToString();
                        break;
                    }
                }

                for (int i = 0; i < Lab_TestItem_Length; i++)
                {
                    if (Lab_TestItem[i].ItemName == "抗压荷重2(N)")
                    {
                        double result;
                        result = Convert.ToDouble(Lab_TestItem[i].ItemResult);
                        table.Cell(5, 4).Range.Text = Math.Round((result / 1000), 1).ToString();
                        break;
                    }
                }

                for (int i = 0; i < Lab_TestItem_Length; i++)
                {
                    if (Lab_TestItem[i].ItemName == "抗压强度2(MPa)")
                    {
                        table.Cell(5, 5).Range.Text = Math.Round(Convert.ToDouble(Lab_TestItem[i].ItemResult), 1).ToString();
                        break;
                    }
                }

                for (int i = 0; i < Lab_TestItem_Length; i++)
                {
                    if (Lab_TestItem[i].ItemName == "抗压荷重3(N)")
                    {
                        double result;
                        result = Convert.ToDouble(Lab_TestItem[i].ItemResult);
                        table.Cell(5, 6).Range.Text = Math.Round((result / 1000), 1).ToString();
                        break;
                    }
                }

                for (int i = 0; i < Lab_TestItem_Length; i++)
                {
                    if (Lab_TestItem[i].ItemName == "抗压强度3(MPa)")
                    {
                        table.Cell(5, 7).Range.Text = Math.Round(Convert.ToDouble(Lab_TestItem[i].ItemResult), 1).ToString();
                        break;
                    }
                }

                for (int i = 0; i < Lab_TestItem_Length; i++)
                {
                    if (Lab_TestItem[i].ItemName == "抗压荷重(KN)")
                    {
                        table.Cell(4, 8).Range.Text = Lab_TestItem[i].ItemResult;
                        break;
                    }
                }

                for (int i = 0; i < Lab_TestItem_Length; i++)
                {
                    if (Lab_TestItem[i].ItemName == "抗压应力（MPa）")
                    {
                        table.Cell(4, 9).Range.Text = Lab_TestItem[i].ItemResult;
                        break;
                    }
                }

            }
            //}
            
            

            //画图
            //获取曲线数据
            string data = GetCurveData(Id);
            Lab_CurveChart jsonData = JsonConvert.DeserializeObject<Lab_CurveChart>(data);

            //获取x,y轴数组
            int[] xArrary = new int[11];//x轴数组
            int[] yArrary = new int[11];//y轴数组
            int xlist_distance = 0;//x轴数组相邻数字间的差值
            int ylist_distance = 0;//y轴数组相邻数字间的差值
            int max_length = 0;//获取DateTime数组(x轴)最大长度
            double maxForce = 0;
            double maxForce1 = 0;
            double maxForce2 = 0;
            double maxForce3 = 0;
            double maxForce4 = 0;
            double maxForce5 = 0;
            double maxForce6 = 0;
            List<double> Force1 = jsonData.Force1;
            List<double> Force2 = jsonData.Force2;
            List<double> Force3 = jsonData.Force3;
            List<double> Force4 = jsonData.Force4;
            List<double> Force5 = jsonData.Force5;
            List<double> Force6 = jsonData.Force6;
            if (jsonData.DateTime1.Count > max_length)
            {
                max_length = jsonData.DateTime1.Count;
            }
            if (jsonData.DateTime2.Count > max_length)
            {
                max_length = jsonData.DateTime2.Count;
            }
            if (jsonData.DateTime3.Count > max_length)
            {
                max_length = jsonData.DateTime3.Count;
            }
            if (jsonData.DateTime4.Count > max_length)
            {
                max_length = jsonData.DateTime4.Count;
            }
            if (jsonData.DateTime5.Count > max_length)
            {
                max_length = jsonData.DateTime5.Count;
            }
            if (jsonData.DateTime6.Count > max_length)
            {
                max_length = jsonData.DateTime6.Count;
            }

            if (max_length < 1000)
            {
                int num = max_length / 10 + 1;
                xlist_distance = num;
            }
            if (max_length >= 1000)
            {
                int num = max_length / 100 + 1;
                xlist_distance = num * 10;
            }
            for (int i = 0; i < 11; i++)//得到x轴数组
            {
                xArrary[i] = xlist_distance * i;
            }

            foreach (double force in Force1)//获取各个force列表的最大值
            {
                if (force > maxForce1)
                {
                    maxForce1 = force;
                }
            }
            foreach (double force in Force2)
            {
                if (force > maxForce2)
                {
                    maxForce2 = force;
                }
            }
            foreach (double force in Force3)
            {
                if (force > maxForce3)
                {
                    maxForce3 = force;
                }
            }
            foreach (double force in Force4)
            {
                if (force > maxForce4)
                {
                    maxForce4 = force;
                }
            }
            foreach (double force in Force5)
            {
                if (force > maxForce5)
                {
                    maxForce5 = force;
                }
            }
            foreach (double force in Force6)
            {
                if (force > maxForce6)
                {
                    maxForce6 = force;
                }
            }
            if (maxForce1 > maxForce)
            {
                maxForce = maxForce1;
            }
            if (maxForce2 > maxForce)
            {
                maxForce = maxForce2;
            }
            if (maxForce3 > maxForce)
            {
                maxForce = maxForce3;
            }
            if (maxForce4 > maxForce)
            {
                maxForce = maxForce4;
            }
            if (maxForce5 > maxForce)
            {
                maxForce = maxForce5;
            }
            if (maxForce6 > maxForce)
            {
                maxForce = maxForce6;
            }
            if (maxForce < 100)
            {
                int num = (int)maxForce / 10 + 1;
                ylist_distance = num;
            }
            if (maxForce >= 100 && maxForce < 1000)
            {
                int num = (int)maxForce / 10 + 1;
                ylist_distance = num;
            }
            if (maxForce >= 1000 && maxForce < 10000)
            {
                int num = (int)maxForce / 100 + 1;
                ylist_distance = num * 10;
            }
            if (maxForce >= 10000 && maxForce < 100000)
            {
                int num = (int)maxForce / 1000 + 1;
                ylist_distance = num * 100;
            }
            if (maxForce >= 100000 && maxForce < 1000000)
            {
                int num = (int)maxForce / 10000 + 1;
                ylist_distance = num * 1000;
            }
            if (maxForce >= 1000000)
            {
                int num = (int)maxForce / 100000 + 1;
                ylist_distance = num * 10000;
            }
            for (int i = 0; i < 11; i++)
            {
                yArrary[i] = i * ylist_distance;//得到y轴数组
            }
                

            //获取曲线的坐标值，并调用MyTool.Lab_PrintChart.Paint绘制、保存曲线图片
            string datas = PrintChartPoints(jsonData, max_length, (int)maxForce);
            Lab_PrintPoint points = JsonConvert.DeserializeObject<Lab_PrintPoint>(datas);//points装载了一个试验所有曲线的所有点的坐标
            Lab_PrintChart print = new MyTool.Lab_PrintChart();
            print.Paint(path, points, xArrary, yArrary);//xArrary、yArrary分别装载x轴和y轴的坐标

            if (curveNum > 0)
            {
                string FileName = "E:\\jtjsypt_data\\YuHuan_TCMIS\\lab\\curve1.jpg";//图片所在路径

                object LinkToFile = false;
                object SaveWithDocument = true;
                object range = wordDoc.Paragraphs.Last.Range;//图片插入位置，最后一行
                wordDoc.Application.ActiveDocument.InlineShapes.AddPicture(FileName, ref LinkToFile, ref SaveWithDocument, ref range);// ref Anchor
                curveNum--;
            }
            if (curveNum > 0)
            {
                string FileName = "E:\\jtjsypt_data\\YuHuan_TCMIS\\lab\\curve2.jpg";//图片所在路径

                object LinkToFile = false;
                object SaveWithDocument = true;
                object range = wordDoc.Paragraphs.Last.Range;//图片插入位置，最后一行
                wordDoc.Application.ActiveDocument.InlineShapes.AddPicture(FileName, ref LinkToFile, ref SaveWithDocument, ref range);// ref Anchor
                curveNum--;
            }
            if (curveNum > 0)
            {
                string FileName = "E:\\jtjsypt_data\\YuHuan_TCMIS\\lab\\curve3.jpg";//图片所在路径

                object LinkToFile = false;
                object SaveWithDocument = true;
                object range = wordDoc.Paragraphs.Last.Range;//图片插入位置，最后一行
                wordDoc.Application.ActiveDocument.InlineShapes.AddPicture(FileName, ref LinkToFile, ref SaveWithDocument, ref range);// ref Anchor
                curveNum--;
            }
            if (curveNum > 0)
            {
                string FileName = "E:\\jtjsypt_data\\YuHuan_TCMIS\\lab\\curve4.jpg";//图片所在路径

                object LinkToFile = false;
                object SaveWithDocument = true;
                object range = wordDoc.Paragraphs.Last.Range;//图片插入位置，最后一行
                wordDoc.Application.ActiveDocument.InlineShapes.AddPicture(FileName, ref LinkToFile, ref SaveWithDocument, ref range);// ref Anchor
                curveNum--;
            }
            if (curveNum > 0)
            {
                string FileName = "E:\\jtjsypt_data\\YuHuan_TCMIS\\lab\\curve5.jpg";//图片所在路径

                object LinkToFile = false;
                object SaveWithDocument = true;
                object range = wordDoc.Paragraphs.Last.Range;//图片插入位置，最后一行
                wordDoc.Application.ActiveDocument.InlineShapes.AddPicture(FileName, ref LinkToFile, ref SaveWithDocument, ref range);// ref Anchor
                curveNum--;
            }
            if (curveNum > 0)
            {
                string FileName = "E:\\jtjsypt_data\\YuHuan_TCMIS\\lab\\curve6.jpg";//图片所在路径

                object LinkToFile = false;
                object SaveWithDocument = true;
                object range = wordDoc.Paragraphs.Last.Range;//图片插入位置，最后一行
                wordDoc.Application.ActiveDocument.InlineShapes.AddPicture(FileName, ref LinkToFile, ref SaveWithDocument, ref range);// ref Anchor
                curveNum--;
            }
            //将wordDoc文档对象的内容保存为DOC文档
            wordDoc.SaveAs(ref path, ref format, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            //关闭wordDoc文档对象
            wordDoc.Close(ref Nothing, ref Nothing, ref Nothing);
            //关闭wordApp组件对象
            wordApp.Quit(ref Nothing, ref Nothing, ref Nothing);
            Console.WriteLine(path + " 创建完毕！");

            return pathString;
        }
       

        /// <summary>
        /// 潘承瑞，由数据库曲线数据经该方法的坐标转换，转换为图片大小为800X270,坐标平面距离左右上下边距分别为60,20,10,20的坐标
        /// </summary>
        /// <param name="jsonData">类型为MyModel.Lab_CurveChart,装载曲线数据</param>
        /// <param name="max_length">该试验中的几条曲线长度的最大值</param>
        /// <param name="maxForce">该试验中的几条曲线力的最大值</param>
        /// <returns>该试验下所有曲线转换以后的坐标</returns>
        public string PrintChartPoints(Lab_CurveChart jsonData, int max_length,int maxForce)
        {
            List<double> Force1 = jsonData.Force1;
            List<double> Force2 = jsonData.Force2;
            List<double> Force3 = jsonData.Force3;
            List<double> Force4 = jsonData.Force4;
            List<double> Force5 = jsonData.Force5;
            List<double> Force6 = jsonData.Force6;
            Lab_PrintPoint points = new Lab_PrintPoint();
            points.Lab_CurveType = jsonData.Lab_CurveType;
            points.curveNum = jsonData.curveNum;
            double pixel_perDatetime = 720.0 / max_length;//DateTime数组中一个值代表图表中多少个像素
            double pixel_perForce = 240.0/maxForce;//Force数组中一个单位的力代表图表中多少个像素
            
            if (points.curveNum != 0)
            {
                //一号曲线
                if (jsonData.curve1Flag!=0)
                {
                    if (max_length <= 720)
                    {

                        double lastPixel = 0.0;//图表中上一个点距离x轴0点的像素
                        double currentPixel = 0.0;//图表中当前点距离x轴0点的像素
                        for (int i = 0; i < Force1.Count; i++)
                        {
                            currentPixel = lastPixel + pixel_perDatetime;
                            if ((int)currentPixel == i + 1)
                            {
                                points.x_Arrary1.Add(i + 60);//x轴0点从距离左边距60开始
                                int value = (int)(Force1[i] * pixel_perForce);
                                points.y_Arrary1.Add(250 - value);//y轴0点从距离上边距250开始

                            }
                            else
                            {
                                points.x_Arrary1.Add((int)currentPixel + 60);//若不相等，跳过图表中的某个像素
                                int value = (int)(Force1[i] * pixel_perForce);
                                points.y_Arrary1.Add(250 - value);

                            }
                            lastPixel = currentPixel;
                        }

                    }
                    else//max_length >720
                    {
                        double lastPoint = 0.0;//数据库curve表中的上一个点
                        double currentPoint = 0.0;//数据库curve表中的当前点
                        double Datetime_perpixel_1 = Force1.Count / 720.0;//图表中一个像素代表DateTime数组中多少个点
                        int intDatetime_perpixel = (int)Datetime_perpixel_1;//图表中一个像素代表DateTime数组中多少个点，取整
                        lastPoint = -Datetime_perpixel_1;
                        for (int i = 0; i < 720; i++)
                        {
                            currentPoint = lastPoint + Datetime_perpixel_1;
                            if ((int)currentPoint == intDatetime_perpixel * (i + 1))
                            {
                                points.x_Arrary1.Add(i + 60);
                                int value = (int)(Force1[(int)currentPoint] * pixel_perForce);
                                points.y_Arrary1.Add(250 - value);
                            }
                            else
                            {
                                points.x_Arrary1.Add(i + 60);
                                int value = (int)(Force1[(int)currentPoint] * pixel_perForce);
                                points.y_Arrary1.Add(250 - value);
                            }
                            
                            lastPoint = currentPoint;
                        }
                    }
                }

                //二号曲线
                if (jsonData.curve2Flag != 0)
                {
                    if (max_length <= 720)
                    {

                        double lastPixel = 0.0;//图表中上一个点距离x轴0点的像素
                        double currentPixel = 0.0;//图表中当前点距离x轴0点的像素
                        for (int i = 0; i < Force2.Count; i++)
                        {
                            currentPixel = lastPixel + pixel_perDatetime;
                            if ((int)currentPixel == i + 1)
                            {
                                points.x_Arrary2.Add(i + 60);//x轴0点从距离左边距60开始
                                int value = (int)(Force2[i] * pixel_perForce);
                                points.y_Arrary2.Add(250 - value);//y轴0点从距离上边距250开始

                            }
                            else
                            {
                                points.x_Arrary2.Add((int)currentPixel + 60);//若不相等，跳过图表中的某个像素
                                int value = (int)(Force2[i] * pixel_perForce);
                                points.y_Arrary2.Add(250 - value);

                            }
                            lastPixel = currentPixel;
                        }

                    }
                    else//max_length >720
                    {
                        double lastPoint = 0.0;//数据库curve表中的上一个点
                        double currentPoint = 0.0;//数据库curve表中的当前点
                        double Datetime_perpixel_2 = Force2.Count / 720.0;//图表中一个像素代表DateTime数组中多少个点
                        int intDatetime_perpixel = (int)Datetime_perpixel_2;//图表中一个像素代表DateTime数组中多少个点，取整
                        lastPoint = -Datetime_perpixel_2;
                        for (int i = 0; i < 720; i++)
                        {
                            currentPoint = lastPoint + Datetime_perpixel_2;
                            if ((int)currentPoint == intDatetime_perpixel * (i + 1))
                            {
                                points.x_Arrary2.Add(i + 60);
                                int value = (int)(Force2[(int)currentPoint] * pixel_perForce);
                                points.y_Arrary2.Add(250 - value);
                            }
                            else
                            {
                                points.x_Arrary2.Add(i + 60);
                                int value = (int)(Force2[(int)currentPoint] * pixel_perForce);
                                points.y_Arrary2.Add(250 - value);
                            }

                            lastPoint = currentPoint;
                        }
                    }
                }

                //三号曲线
                if (jsonData.curve3Flag != 0)
                {
                    if (max_length <= 720)
                    {

                        double lastPixel = 0.0;//图表中上一个点距离x轴0点的像素
                        double currentPixel = 0.0;//图表中当前点距离x轴0点的像素
                        for (int i = 0; i < Force3.Count; i++)
                        {
                            currentPixel = lastPixel + pixel_perDatetime;
                            if ((int)currentPixel == i + 1)
                            {
                                points.x_Arrary3.Add(i + 60);//x轴0点从距离左边距60开始
                                int value = (int)(Force3[i] * pixel_perForce);
                                points.y_Arrary3.Add(250 - value);//y轴0点从距离上边距250开始

                            }
                            else
                            {
                                points.x_Arrary3.Add((int)currentPixel + 60);//若不相等，跳过图表中的某个像素
                                int value = (int)(Force3[i] * pixel_perForce);
                                points.y_Arrary3.Add(250 - value);

                            }
                            lastPixel = currentPixel;
                        }

                    }
                    else//max_length >720
                    {
                        double lastPoint = 0.0;//数据库curve表中的上一个点
                        double currentPoint = 0.0;//数据库curve表中的当前点
                        double Datetime_perpixel_3 = Force3.Count / 720.0;//图表中一个像素代表DateTime数组中多少个点
                        int intDatetime_perpixel = (int)Datetime_perpixel_3;//图表中一个像素代表DateTime数组中多少个点，取整
                        lastPoint = -Datetime_perpixel_3;
                        for (int i = 0; i < 720; i++)
                        {
                            currentPoint = lastPoint + Datetime_perpixel_3;
                            if ((int)currentPoint == intDatetime_perpixel * (i + 1))
                            {
                                points.x_Arrary3.Add(i + 60);
                                int value = (int)(Force3[(int)currentPoint] * pixel_perForce);
                                points.y_Arrary3.Add(250 - value);
                            }
                            else
                            {
                                points.x_Arrary3.Add(i + 60);
                                int value = (int)(Force3[(int)currentPoint] * pixel_perForce);
                                points.y_Arrary3.Add(250 - value);
                            }

                            lastPoint = currentPoint;
                        }
                    }
                }

                //四号曲线
                if (jsonData.curve4Flag != 0)
                {
                    if (max_length <= 720)
                    {

                        double lastPixel = 0.0;//图表中上一个点距离x轴0点的像素
                        double currentPixel = 0.0;//图表中当前点距离x轴0点的像素
                        for (int i = 0; i < Force4.Count; i++)
                        {
                            currentPixel = lastPixel + pixel_perDatetime;
                            if ((int)currentPixel == i + 1)
                            {
                                points.x_Arrary4.Add(i + 60);//x轴0点从距离左边距60开始
                                int value = (int)(Force4[i] * pixel_perForce);
                                points.y_Arrary4.Add(250 - value);//y轴0点从距离上边距250开始

                            }
                            else
                            {
                                points.x_Arrary4.Add((int)currentPixel + 60);//若不相等，跳过图表中的某个像素
                                int value = (int)(Force4[i] * pixel_perForce);
                                points.y_Arrary4.Add(250 - value);

                            }
                            lastPixel = currentPixel;
                        }

                    }
                    else//max_length >720
                    {
                        double lastPoint = 0.0;//数据库curve表中的上一个点
                        double currentPoint = 0.0;//数据库curve表中的当前点
                        double Datetime_perpixel_4 = Force4.Count / 720.0;//图表中一个像素代表DateTime数组中多少个点
                        int intDatetime_perpixel = (int)Datetime_perpixel_4;//图表中一个像素代表DateTime数组中多少个点，取整
                        lastPoint = -Datetime_perpixel_4;
                        for (int i = 0; i < 720; i++)
                        {
                            currentPoint = lastPoint + Datetime_perpixel_4;
                            if ((int)currentPoint == intDatetime_perpixel * (i + 1))
                            {
                                points.x_Arrary4.Add(i + 60);
                                int value = (int)(Force4[(int)currentPoint] * pixel_perForce);
                                points.y_Arrary4.Add(250 - value);
                            }
                            else
                            {
                                points.x_Arrary4.Add(i + 60);
                                int value = (int)(Force4[(int)currentPoint] * pixel_perForce);
                                points.y_Arrary4.Add(250 - value);
                            }

                            lastPoint = currentPoint;
                        }
                    }
                }

                //五号曲线
                if (jsonData.curve5Flag != 0)
                {
                    if (max_length <= 720)
                    {

                        double lastPixel = 0.0;//图表中上一个点距离x轴0点的像素
                        double currentPixel = 0.0;//图表中当前点距离x轴0点的像素
                        for (int i = 0; i < Force5.Count; i++)
                        {
                            currentPixel = lastPixel + pixel_perDatetime;
                            if ((int)currentPixel == i + 1)
                            {
                                points.x_Arrary5.Add(i + 60);//x轴0点从距离左边距60开始
                                int value = (int)(Force5[i] * pixel_perForce);
                                points.y_Arrary5.Add(250 - value);//y轴0点从距离上边距250开始

                            }
                            else
                            {
                                points.x_Arrary5.Add((int)currentPixel + 60);//若不相等，跳过图表中的某个像素
                                int value = (int)(Force5[i] * pixel_perForce);
                                points.y_Arrary5.Add(250 - value);

                            }
                            lastPixel = currentPixel;
                        }

                    }
                    else//max_length >720
                    {
                        double lastPoint = 0.0;//数据库curve表中的上一个点
                        double currentPoint = 0.0;//数据库curve表中的当前点
                        double Datetime_perpixel_5 = Force5.Count / 720.0;//图表中一个像素代表DateTime数组中多少个点
                        int intDatetime_perpixel = (int)Datetime_perpixel_5;//图表中一个像素代表DateTime数组中多少个点，取整
                        lastPoint = -Datetime_perpixel_5;
                        for (int i = 0; i < 720; i++)
                        {
                            currentPoint = lastPoint + Datetime_perpixel_5;
                            if ((int)currentPoint == intDatetime_perpixel * (i + 1))
                            {
                                points.x_Arrary5.Add(i + 60);
                                int value = (int)(Force5[(int)currentPoint] * pixel_perForce);
                                points.y_Arrary5.Add(250 - value);
                            }
                            else
                            {
                                points.x_Arrary5.Add(i + 60);
                                int value = (int)(Force5[(int)currentPoint] * pixel_perForce);
                                points.y_Arrary5.Add(250 - value);
                            }

                            lastPoint = currentPoint;
                        }
                    }
                }

                //六号曲线
                if (jsonData.curve6Flag != 0)
                {
                    if (max_length <= 720)
                    {

                        double lastPixel = 0.0;//图表中上一个点距离x轴0点的像素
                        double currentPixel = 0.0;//图表中当前点距离x轴0点的像素
                        for (int i = 0; i < Force6.Count; i++)
                        {
                            currentPixel = lastPixel + pixel_perDatetime;
                            if ((int)currentPixel == i + 1)
                            {
                                points.x_Arrary6.Add(i + 60);//x轴0点从距离左边距60开始
                                int value = (int)(Force6[i] * pixel_perForce);
                                points.y_Arrary6.Add(250 - value);//y轴0点从距离上边距250开始

                            }
                            else
                            {
                                points.x_Arrary6.Add((int)currentPixel + 60);//若不相等，跳过图表中的某个像素
                                int value = (int)(Force6[i] * pixel_perForce);
                                points.y_Arrary6.Add(250 - value);

                            }
                            lastPixel = currentPixel;
                        }

                    }
                    else//max_length >720
                    {
                        double lastPoint = 0.0;//数据库curve表中的上一个点
                        double currentPoint = 0.0;//数据库curve表中的当前点
                        double Datetime_perpixel_6 = Force6.Count / 720.0;//图表中一个像素代表DateTime数组中多少个点
                        int intDatetime_perpixel = (int)Datetime_perpixel_6;//图表中一个像素代表DateTime数组中多少个点，取整
                        lastPoint = -Datetime_perpixel_6;
                        for (int i = 0; i < 720; i++)
                        {
                            currentPoint = lastPoint + Datetime_perpixel_6;
                            if ((int)currentPoint == intDatetime_perpixel * (i + 1))
                            {
                                points.x_Arrary6.Add(i + 60);
                                int value = (int)(Force6[(int)currentPoint] * pixel_perForce);
                                points.y_Arrary6.Add(250 - value);
                            }
                            else
                            {
                                points.x_Arrary6.Add(i + 60);
                                int value = (int)(Force6[(int)currentPoint] * pixel_perForce);
                                points.y_Arrary6.Add(250 - value);
                            }

                            lastPoint = currentPoint;
                        }
                    }
                }

            }
            string json = JsonConvert.SerializeObject(points);
            return json;
        }
    }
}

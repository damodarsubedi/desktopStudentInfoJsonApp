using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork_Damodar
{
    class Student
    {
        private string _filePath = "studentInformation.json";
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Gender { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string ProgramEnrolled { get; set; }

        public void Create(Student info)
        {
            Random random = new Random();
            info.Id = random.Next(100, 999);
            string data = JsonConvert.SerializeObject(info, Formatting.None);
            Utility.WriteToTextFile(_filePath, data);
        }
        public List<Student> List()
        {
            string read = Utility.ReadFromTextFile(_filePath);
            if(read != null)
            {
                List<Student> list = JsonConvert.DeserializeObject<List<Student>>(read);
                return list;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Nothing to read from file", "Error on Loading");
            }
            return null;
        }

        public Student Update(int id)
        {
            Student obj = new Student();
            return obj;
        }

        public void Update(Student info)
        {
            List<Student> list = List();
            Student s = list.Where(x => x.Id == info.Id).FirstOrDefault();
            list.Remove(s);
            list.Add(info);
            string data = JsonConvert.SerializeObject(list, Formatting.None);
            Utility.WriteToTextFile(_filePath, data,false);

        }
        public void Delete(int id)
        {
            List<Student> list = List();
            Student s = list.Where(x => x.Id == id).FirstOrDefault();
            list.Remove(s);
            int count = list.Count;
            string data = JsonConvert.SerializeObject(list, Formatting.None);
            Utility.WriteToTextFile(_filePath, data, false);

        }
        public Student Detail(int id)
        {
            Student obj = new Student();
            return obj;
        }

        internal List<Student> Sort(object listStudents, string v)
        {
            throw new NotImplementedException();
        }
        public List<Student> Sort(List<Student> listStudents, string sortType)
        {
            if (sortType == "Name")
            {
                string[] arr = new string[listStudents.Count];

                //initializing Aray for students
                for (var i = 0; i < listStudents.Count; i++)
                {
                    arr[i] = listStudents[i].Name;
                }

                //Using Bubble sorting algorithm
                for (int i = arr.Length - 1; i > 0; i--)
                {
                    for (int j = 0; j <= i - 1; j++)
                    {
                        if (arr[j].CompareTo(arr[j + 1]) > 0) // checking alphabets
                        {
                            //if greater element swap
                            string big = arr[j];
                            arr[j] = arr[j + 1];
                            arr[j + 1] = big;

                            //if name is to be swapped swapping whole data 
                            Student bigList = listStudents[j];
                            listStudents[j] = listStudents[j + 1];
                            listStudents[j + 1] = bigList;
                        }
                    }
                }
            }
            else
            {
                DateTime[] arr = new DateTime[listStudents.Count];

                //initializing Aray for students
                for (var i = 0; i < listStudents.Count; i++)
                {
                    arr[i] = listStudents[i].RegistrationDate;
                }

                //Using Bubble sorting algorithm
                for (int i = arr.Length - 1; i > 0; i--)
                {
                    for (int j = 0; j <= i - 1; j++)
                    {
                        if (arr[j].CompareTo(arr[j + 1]) > 0) //comparing which is geater by date
                        {
                            //if greater element swap
                            DateTime big = arr[j];
                            arr[j] = arr[j + 1];
                            arr[j + 1] = big;

                            //if date is to be swapped swapping whole data 
                            Student bigList = listStudents[j];
                            listStudents[j] = listStudents[j + 1];
                            listStudents[j + 1] = bigList;
                        }
                    }
                }
            }
            return listStudents;
        }
    }
}

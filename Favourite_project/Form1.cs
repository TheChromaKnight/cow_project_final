using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace Favourite_project
{
    public partial class Form1 : Form
    {
        //Globális adattagok
        string connection_string = "SERVER=localhost;DATABASE=cow_datab;UID=root;PWD=;Convert Zero Datetime=True";
        MySqlDataAdapter adapter;
        MySqlCommand cmd;
        MySqlConnection conn;
        DataTable dt;
        DataSet ds;
        Regex regex;
        DateTime datet;


        public Form1()
        {
            InitializeComponent();

            med_combobox_fill();
            cow_type_combobox_fill();
            cow_sex_combobox_fill();
            cow_age_type_combobox_fill();
            cow_color_combobox_fill();
            insemination_cow_id_combobox_fill();
            insemination_type_combobox_fill();
            list_cow_id_combobox_fill();
            list_medicine_id_combobox_fill();


            show_med_type_table();           
            show_med_table();
            show_color_table();
            show_species_table();
            show_sex_table();
            show_cow_table();
            show_insemination_type_table();
            show_insemination_table();
            show_list_table();






        }

        //Globális metódusok amik bárhol használhatóak
        
        private void global_empty_box()
        {
            MessageBox.Show("Üres Mező!");
        }

        private void global_invalid_form()
        {
            MessageBox.Show("Helytelen azonosító formátum");
        }

        private void global_invalid_date_form()
        {
            MessageBox.Show("Helytelen dátum formátum");
        }

        private void global_fields_are_equal()
        {
            MessageBox.Show("A két mező nem tartalmazhatja ugyanazt az adatot!");
        }
            

        private void global_success_insert_box()
        {
            MessageBox.Show("Sikeres felvitel!");
        }

        private void global_success_update_box()
        {
            MessageBox.Show("Sikeres Módosítás!");
        }

        private void global_success_delete_box()
        {
            MessageBox.Show("Sikeres törlés!");
        }

        


                                                                    //Gyógyszer típus rész!!!!!!!!!!!!!
        //A mezők kiürítése így egyszerűbb
        private void clear_med_type_fields()
        {
            tb_med_type.Text = "";
        }

       
        //MISSCLICK useless
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        
        //Tábla megjelenítése/használható frissítésre
        private void show_med_type_table()
        {
            string sql = "SELECT * FROM medicine_type";


            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();
                adapter = new MySqlDataAdapter(sql, conn);
                dt = new DataTable();
                adapter.Fill(dt);


                dgv_med_type.DataSource = dt;


            }
        }

        // Feltöltés gombra kattintás
        private void btn_upload_Click(object sender, EventArgs e)
        {
            string name;
            name = tb_med_type.Text.Trim();
            if (tb_med_type.Text != "")
            {
                med_type_insert(name);
            }
            else
            {
                global_empty_box();
            }



        }

        //Feltöltés metódus
        private void med_type_insert(string name)
        {
            string sql   = "Insert into medicine_type VALUES(NULL,'" + name + "')";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();

                global_success_insert_box();

                
                show_med_type_table();

                //Habár nem ide tartozik de ez szükséges a frissítéshez a Gyógyszerek menüpont alatt máskülönben nem jelenne meg az új adat a checkboxban
                med_combobox_fill();

            }
        }

        //Módosítás metódus
        private void med_type_update(string med_name, int id)
        {
            string sql = "UPDATE medicine_type SET medicine_type_name = '"+med_name+"' WHERE medicine_type.medicine_type_id = "+id+";";

            using (conn= new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);
                
                cmd.ExecuteNonQuery();

                global_success_update_box();

                clear_med_type_fields();

                med_combobox_fill();

            }

        }

        //Módosít gombra kattintásra módosítás függvény meghívása azonban index ellenőrzése hogy van-e kiválasztva bármi
        private void btn_update_Click(object sender, EventArgs e)
        {
            int id;
            string name;
            
            id = Convert.ToInt32(dgv_med_type.CurrentRow.Cells[0].Value);
            name = tb_med_type.Text;

            med_type_update(name, id);
            
            show_med_type_table();
            
        }

        //Törlés metódus
        private void med_type_delete(int id)
        {
            string sql = "DELETE FROM medicine_type WHERE medicine_type_id = " + id + "";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();

                global_success_delete_box();

                med_combobox_fill();



            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            
                if (MessageBox.Show("Biztosan törölni szeretnéd?", "Törlés", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int id;
                    id = Convert.ToInt32(dgv_med_type.CurrentRow.Cells[0].Value);

                    med_type_delete(id);

                    clear_med_type_fields();

                    show_med_type_table();
                }
               
            


        }




        //Useless Missclick
        private void dgv_med_type_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //Dupla kattintásra textbox feltöltése a kiválasztott elem adatával

        private void dgv_med_type_DoubleClick(object sender, EventArgs e)
        {
            if (dgv_med_type.CurrentRow.Index != -1)
            {
                tb_med_type.Text = dgv_med_type.CurrentRow.Cells[1].Value.ToString();


            }
        }

        //Useless Missclick
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

                                                                                    //Gyógyszer típus rész vége!!!!!!!!!!!!!





                                                                                    //Gyógyszer rész !!!!!!!!!!!!!!!!!!!!!
        //Mezők kiürítése
        private void clear_med_fields()
        {
            cb_med.SelectedIndex = 0;
            tb_med_name.Text = "";
        }

        // Combobox feltöltése
        private void med_combobox_fill()
        {
            string sql = "SELECT * FROM medicine_type";
            ds = new DataSet();

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql,conn);
                adapter = new MySqlDataAdapter();
                
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            

                cb_med.DisplayMember = "medicine_type_name";
                cb_med.ValueMember = "medicine_type_id";
                cb_med.DataSource = ds.Tables[0];
                


            }
        }

        // Gyógyszer rész táblája
        private void show_med_table()
        {
            string sql = "SELECT medicine_id,medicine_name,medicine_type_id,medicine_type_name FROM medicine INNER JOIN medicine_type ON medicine_type_id = medicine_medicine_type_id";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                adapter = new MySqlDataAdapter(sql,conn);
                dt = new DataTable();

                adapter.Fill(dt);

                dgv_med.DataSource = dt;



            }
        }


        //Beszúrás metódus
        private void med_insert(string name,int id)
        {
            string sql = "INSERT INTO medicine VALUES (NULL, '"+name+"', "+id+");";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();

               

            }

        }


        //Felvitel gomb
        private void med_btn_insert_Click(object sender, EventArgs e)
        {

            //Ahhoz hogy megkapjuk a feltöltéshez kívánt id-t le kell kérdeznünk a combobox "value"-t, majd át kell konvertálnunk int típusúba, hogy adatbázisban megfelelően működjön
            int id;
            string medicine_name;

            id = Convert.ToInt32(cb_med.SelectedValue);
            medicine_name = tb_med_name.Text.Trim();

            //MessageBox.Show(convertable_id);
            if (tb_med_name.Text != "" && cb_med.SelectedIndex > -1)
            {
                med_insert(medicine_name, id);
                
                global_success_insert_box();

                clear_med_fields();

                show_med_table();

                list_medicine_id_combobox_fill();

            }
            else
                global_empty_box();
        }

        //Módosítás metódus
        private void med_update(string name,int type_id,int id)
        {
            string sql = "UPDATE medicine SET medicine_name = '"+name+"',medicine_medicine_type_id = "+type_id+" WHERE medicine_id = "+id+"";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();

               
            }



        }

        //Dupla kattintásra feltöltjük a kiválasztott sornak az adataival a mezőket
        private void dgv_med_DoubleClick(object sender, EventArgs e)
        {
            if(dgv_med.CurrentRow.Index != -1)
            {
                tb_med_name.Text = dgv_med.CurrentRow.Cells[1].Value.ToString();
                cb_med.SelectedValue = dgv_med.CurrentRow.Cells[2].Value;
               // MessageBox.Show(cb_med.SelectedValue.ToString());
            }
        }

        //Módosítás gomb
        private void med_btn_update_Click(object sender, EventArgs e)
        {
            string name;
            int type_id;
            int id;

            name = tb_med_name.Text;
            type_id = Convert.ToInt32(cb_med.SelectedValue);
            id = Convert.ToInt32(dgv_med.CurrentRow.Cells[0].Value);

            med_update(name, type_id, id);
           
            global_success_update_box();

            clear_med_fields();

            show_med_table();

            list_medicine_id_combobox_fill();

        }

        //Törlés metódus
        private void med_delete(int id)
        {
            string sql = "Delete FROM medicine WHERE medicine_id = " + id + ";";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();

                
            }
        }

        //Törlés gomb
        private void med_btn_delete_Click(object sender, EventArgs e)
        {
            
                if (MessageBox.Show("Biztosan törölni szeretnéd?", "Törlés", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int id;

                    id = Convert.ToInt32(dgv_med.CurrentRow.Cells[0].Value);

                    med_delete(id);
                
                    clear_med_fields();

                    global_success_delete_box();

                    show_med_table();

                    list_medicine_id_combobox_fill();
                }
                
            
        }



                                                                    //Gyógyszer rész vége



                                                                    //Fajta szín rész kezdete!!!!!!!!!!!!!!!!!!!!!!!!!!

        //Mezők kiürítése
        private void clear_color_fields()
        {
            tb_colors_name.Text = "";
        }

        //Beszúrás metódus
        private void color_insert(string name)
        {
            string sql = "INSERT INTO cow_color VALUES (null,'"+name+"')";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();


            }
        }

        //Táblázat mutatása/frissítése
        private void show_color_table()
        {
            string sql = "SELECT * FROM cow_color";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();
                adapter = new MySqlDataAdapter(sql, conn);
                dt = new DataTable();

                adapter.Fill(dt);

                dgv_color.DataSource = dt;
            }
        }

        //Kattintásra beszúrás
        private void btn_color_insert_Click(object sender, EventArgs e)
        {
            string name;
            name = tb_colors_name.Text.Trim();

            if (tb_colors_name.Text != "")
            {
                color_insert(name);

                clear_color_fields();

                global_success_insert_box();

                show_color_table();

                cow_color_combobox_fill();


            }
            else
                global_empty_box();
        }

        //Frissítés metódus
        private void color_update(int id,string name)
        {
            string sql = "UPDATE cow_color SET cow_color_name = '"+name+"' WHERE cow_color_id = "+id+"";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();
            }
        }

        //kattintásra frissítés
        private void btn_color_update_Click(object sender, EventArgs e)
        {
            if (tb_colors_name.Text != "")
            {
                int id;
                string name;

                id = Convert.ToInt32(dgv_color.CurrentRow.Cells[0].Value);
                name = tb_colors_name.Text;

                color_update(id, name);
              
                clear_color_fields();

                global_success_update_box();

                show_color_table();

                cow_color_combobox_fill();


            }
            else
                global_empty_box();
        }

        //Dupla kattintásra beszúrjuk a kiválasztott sor adatait a mezőkbe
        private void dgv_color_DoubleClick(object sender, EventArgs e)
        {
            if(dgv_color.CurrentRow.Index != -1)
            {
                tb_colors_name.Text = dgv_color.CurrentRow.Cells[1].Value.ToString();
            }
        }

        //Törlés metódus
        private void color_delete(int id)
        {
            string sql = "DELETE FROM cow_color WHERE cow_color_id = "+id+"";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();
            }
        }

        //Kattintásra törlés
        private void btn_color_delete_Click(object sender, EventArgs e)
        {
            
                if(MessageBox.Show("Biztosan törölni szeretnéd?","Törlés",MessageBoxButtons.YesNo) ==DialogResult.Yes)
                {
                    int id;
                    id = Convert.ToInt32(dgv_color.CurrentRow.Cells[0].Value);
                    color_delete(id);
               
                    clear_color_fields();

                    global_success_delete_box();

                    show_color_table();
                }
            


        }

                                                                    //Fajta szín rész vége!!!!!!!!!!!!!!



                                                                    //Fajta rész kezdete!!!!!!!!!!!!!!!!
        
        //Mezők ürítése
        private void clear_species_fields()
        {
            tb_species_name.Text = "";
            
        }

        // Táblázat mutatása/frissítése metódus
        private void show_species_table()
        {
            string sql = "SELECT cow_type_id, cow_type_name FROM cow_type";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                adapter = new MySqlDataAdapter(sql, conn);

                dt = new DataTable();

                adapter.Fill(dt);

                dgv_species.DataSource = dt;
            }
        }

        

        //Beszúrás metódus
        private void species_insert(string name)
        {
            string sql = "Insert into cow_type VALUES (null, '" + name + "')";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();

            }
        }

        //Kattintásra beszúrás
        private void btn_species_insert_Click(object sender, EventArgs e)
        {

            if (tb_species_name.Text != "")
            {
                
                string name;

                name = tb_species_name.Text;

                species_insert(name);

                cow_type_combobox_fill();

                clear_species_fields();

                global_success_insert_box();

                show_species_table();

                cow_type_combobox_fill();


            }
            else
                global_empty_box();
        }

        //Frissítés metódus
        private void species_update(int id, string name)
        {
            string sql = "UPDATE cow_type SET cow_type_name = '" + name + "' WHERE cow_type_id = "+id+"";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();


            }
        }

        //Dupla kattintásra a táblázat egy során betöltjük ennek az adatait
        private void dgv_species_DoubleClick(object sender, EventArgs e)
        {
            if (dgv_species.CurrentRow.Index != -1)
            {
                tb_species_name.Text = dgv_species.CurrentRow.Cells[1].Value.ToString();
            }
            
        }

        //Kattintásra frissítés
        private void btn_species_update_Click(object sender, EventArgs e)
        {
            if (tb_species_name.Text != "")
            {
                int id;
                string name;
                

                name = tb_species_name.Text.Trim();
                id = Convert.ToInt32(dgv_species.CurrentRow.Cells[0].Value);

                species_update(id, name);

                cow_type_combobox_fill();

                clear_species_fields();

                global_success_update_box();

                show_species_table();

                cow_type_combobox_fill();

            }
            else
                global_empty_box();
        }

        //Törlés metódus
        private void species_delete(int id)
        {
            string sql = "DELETE FROM cow_type WHERE cow_type_id = " + id + ";";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();


            }
        }

        //Kattintásra törlés
        private void btn_species_delete_Click(object sender, EventArgs e)
        {
           
                if (MessageBox.Show("Biztosan törölni szeretnéd?", "Törlés", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int id;

                    id = Convert.ToInt32(dgv_species.CurrentRow.Cells[0].Value);

                    species_delete(id);

                    clear_species_fields();

                    global_success_delete_box();

                    show_species_table();


                }
                
            
        }


                                                                         //Fajta rész vége!!!!!!!!!!!!!!!!!!!!!!!!!!!



                                                                         //Állat nem kezdete!!!!!!!!!!!!!!!!!!!!!!!!!


        public void clear_sex_fields()
        {
            tb_sex_name.Text = "";
        }

        public void show_sex_table()
        {
            string sql = "SELECT * FROM cow_sex";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                adapter = new MySqlDataAdapter(sql, conn);

                dt = new DataTable();

                adapter.Fill(dt);

                 dgv_sex.DataSource = dt;


            }
        }

        public void sex_insert(string name)
        {
            string sql = "INSERT INTO cow_sex VALUES (null, '" + name + "')";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();


            }
        }

        private void btn_sex_insert_Click(object sender, EventArgs e)
        {
            if (tb_sex_name.Text != "")
            {
                string name;

                name = tb_sex_name.Text.Trim();

                sex_insert(name);

                cow_sex_combobox_fill();

                clear_sex_fields();

                global_success_insert_box();

                show_sex_table();

                cow_sex_combobox_fill();

                
            }
            else
                global_empty_box();
        }

        private void sex_update(int id,string name)
        {
            string sql = "UPDATE cow_sex SET cow_sex_name = '" + name + "' WHERE cow_sex_id = " + id + "";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();
            }
        }

        private void btn_sex_update_Click(object sender, EventArgs e)
        {
            if (tb_sex_name.Text != "")
            {
                int id;
                string name;

                id = Convert.ToInt32(dgv_sex.CurrentRow.Cells[0].Value);
                name = tb_sex_name.Text.Trim();

                sex_update(id, name);

                cow_sex_combobox_fill();

                clear_sex_fields();

                global_success_update_box();

                show_sex_table();

                cow_sex_combobox_fill();


            }
            else
                global_empty_box();
        }

        private void sex_delete(int id)
        {
            string sql = "DELETE FROM cow_sex WHERE cow_sex_id = " + id + "";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql,conn);

                cmd.ExecuteNonQuery();
            }
        }

        private void btn_sex_delete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show( "Biztosan szeretnéd törölni?", "törlés",MessageBoxButtons.YesNo) == DialogResult.Yes)
            { 
                int id;

                id = Convert.ToInt32(dgv_sex.CurrentRow.Cells[0].Value);

                sex_delete(id);

                clear_sex_fields();

                global_success_delete_box();

                show_sex_table();
            }
            
        }

        private void dgv_sex_DoubleClick(object sender, EventArgs e)
        {
            tb_sex_name.Text = dgv_sex.CurrentRow.Cells[1].Value.ToString();
        }



                                                                        //Állat nem vége!!!!!!!!!!!!!!!!!!!!!!

                                                                        

                                                                        //Tehenek rész kezdete!!!!!!!!!!!!!!!!!

        private void clear_cow_fields()
        {
            tb_cow_id.Text = "";
            tb_cow_mother_id.Text = "";
            tb_cow_death.Text = "";

            cb_cow_age_type.SelectedIndex = 0;
            cb_cow_sex.SelectedIndex = 0;
            cb_cow_type.SelectedIndex = 0;
            cb_cow_color.SelectedIndex = 0;

            dt_cow_born.Text = "";

            chb_cow_pregnant.CheckState = CheckState.Unchecked;
           

        }
        
        private void cow_type_combobox_fill()
        {
            string sql = "SELECT * FROM cow_type";
            ds = new DataSet();

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                adapter = new MySqlDataAdapter();

                adapter.SelectCommand = cmd;

                adapter.Fill(ds);

                cb_cow_type.DisplayMember = "cow_type_name";
                cb_cow_type.ValueMember = "cow_type_id";
                cb_cow_type.DataSource = ds.Tables[0];
            }
        }

        private void cow_sex_combobox_fill()
        {
            string sql = "SELECT * FROM cow_sex";
            ds = new DataSet();

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                adapter = new MySqlDataAdapter();

                adapter.SelectCommand = cmd;

                adapter.Fill(ds);

                cb_cow_sex.DisplayMember = "cow_sex_name";
                cb_cow_sex.ValueMember = "cow_sex_id";
                cb_cow_sex.DataSource = ds.Tables[0];

            }
        }

        private void cow_age_type_combobox_fill()
        {
            string sql = "SELECT * FROM cow_age_type";
            ds = new DataSet();

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                adapter = new MySqlDataAdapter();

                adapter.SelectCommand = cmd;

                adapter.Fill(ds);

                cb_cow_age_type.DisplayMember = "cow_age_type_name";
                cb_cow_age_type.ValueMember = "cow_age_type_id";
                cb_cow_age_type.DataSource = ds.Tables[0];
            }
        }

        private void cow_color_combobox_fill()
        {
            string sql = "SELECT * FROM cow_color";
            ds = new DataSet();

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                adapter = new MySqlDataAdapter();

                adapter.SelectCommand = cmd;

                adapter.Fill(ds);

                cb_cow_color.DisplayMember = "cow_color_name";
                cb_cow_color.ValueMember = "cow_color_id";
                cb_cow_color.DataSource = ds.Tables[0];
            }
        }

        private void show_cow_table()
        {
            string sql = "SELECT cow_primary_key,cow_id, cow_mother_id, cow_cow_type_id,cow_type_name, cow_cow_sex_id,cow_sex_name, cow_color_id,cow_color_name, cow_birth, cow_death, cow_cow_pregnant_id, cow_pregnant_name, cow_cow_age_type_id, cow_age_type_name FROM cow INNER JOIN cow_age_type ON cow_age_type_id = cow_cow_age_type_id INNER JOIN cow_type ON cow_type_id = cow_cow_type_id INNER JOIN cow_sex ON cow_sex_id = cow_cow_sex_id INNER JOIN cow_color ON cow_color_id = cow_cow_color_id INNER JOIN cow_pregnant ON cow_cow_pregnant_id = cow_pregnant_id ";

            using (conn = new MySqlConnection(connection_string))
            {
               
                adapter = new MySqlDataAdapter(sql,conn);

                dt = new DataTable();

                adapter.Fill(dt);

                dgv_cow.DataSource = dt;
            }
        }

        private void cow_insert(string id, string mother_id, int type_id, int sex_id, int color_id, string birth, string death, int pregnant_id, int age_type_id )
        {
            string sql = "INSERT INTO cow VALUES (null, '"+id+"', '"+mother_id+"', "+type_id+", "+sex_id+", "+color_id+", '"+birth+"', '"+death+"', "+pregnant_id+", "+age_type_id+");";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();

            }
        }


        private void btn_cow_insert_Click(object sender, EventArgs e)
        {
            //Hiba változó hogy ne egyszerre jelenítsük meg a figyelmeztető üzeneteket
            int error_nums = 0;
            string id = "";
            string mother_id = "";

            //Muszáj 10 karakternek lennie máskülönben helytelen a formátum

            

            if(error_nums == 0)
            {
                regex = new Regex(@"^\d{10}$");

                id = tb_cow_id.Text.Trim();
                mother_id = tb_cow_mother_id.Text.Trim();

                if(id == mother_id)
                {
                    error_nums = 1;
                    global_fields_are_equal();
                }


                //Ha helyes a formátum akkor nem csinálunk semmit, ha pedig helytelen akkor a hiba változót 1-re állítjuk be
                if (regex.IsMatch(id) && regex.IsMatch(mother_id))
                {

                }
                else
                {
                    error_nums = 1;
                    global_invalid_form();
                }
            }


            int type_id;
            int sex_id;
            int color_id;
            string birth;
            string death = "";
            int pregnant_id;
            int age_type_id;

            type_id = Convert.ToInt32(cb_cow_type.SelectedValue);
            sex_id = Convert.ToInt32(cb_cow_sex.SelectedValue);
            color_id = Convert.ToInt32(cb_cow_color.SelectedValue);
            birth = dt_cow_born.Value.ToString("yyyy-MM-dd");

            //Mivel ez a rész üresen is maradhat, ezért csak akkor vizsgáljuk meg regex-xel, hogyha ténylegesen tartalmaz valami karaktert
            if (error_nums == 0)
            {
                if (tb_cow_death.Text == "")
                {
                    death = "";

                }
                else if (tb_cow_death.Text.Length > 0)
                {

                    regex = new Regex(@"^\d{4}-\d{1,2}-\d{1,2}$");
                    death = tb_cow_death.Text.Trim();


                    if (regex.IsMatch(death))
                    {

                    }
                    else
                    {
                        error_nums = 1;
                        global_invalid_date_form();

                    }
                }
            }

            if (chb_cow_pregnant.Checked)
            {
                pregnant_id = 1;
            }
            else
                pregnant_id = 0;

            age_type_id = Convert.ToInt32(cb_cow_age_type.SelectedValue);
            
            if (error_nums == 0)
            {
                cow_insert(id, mother_id, type_id, sex_id, color_id, birth, death, pregnant_id, age_type_id);

                clear_cow_fields();

                global_success_insert_box();

                show_cow_table();

                insemination_cow_id_combobox_fill();

                list_cow_id_combobox_fill();

            }
        }

        private void cow_update(string id, string mother_id, int type_id, int sex_id, int color_id, string birth, string death, int pregnant_id, int age_type_id, int prim_key)
        {
            string sql = "UPDATE cow SET cow_id = '" + id + "', cow_mother_id = '" + mother_id + "', cow_cow_type_id = " + type_id + ", cow_cow_sex_id = " + sex_id + ", cow_cow_color_id = " + color_id + ", cow_birth = '" + birth + "', cow_death = '" + death + "', cow_cow_pregnant_id = " + pregnant_id + ", cow_cow_age_type_id = " + age_type_id + " WHERE cow_primary_key = "+prim_key+"";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();

            }
        }

        private void btn_cow_update_Click(object sender, EventArgs e)
        {
            int prim_key;

            prim_key = Convert.ToInt32(dgv_cow.CurrentRow.Cells[0].Value);

            int error_nums = 0;
            string id = "";
            string mother_id = "";

            if(error_nums == 0)
            { 
                regex = new Regex(@"^\d{10}$");

                id = tb_cow_id.Text.Trim();
                mother_id = tb_cow_mother_id.Text.Trim();

                if (id == mother_id)
                {
                    error_nums = 1;
                    global_fields_are_equal();
                }


                //Ha helyes a formátum akkor nem csinálunk semmit, ha pedig helytelen akkor a hiba változót 1-re állítjuk be
                if (regex.IsMatch(id) && regex.IsMatch(mother_id))
                {

                }
                else
                {
                    error_nums = 1;
                    global_invalid_form();
                }
            }


            int type_id;
            int sex_id;
            int color_id;
            string birth;
            string death = "";
            int pregnant_id;
            int age_type_id;

            type_id = Convert.ToInt32(cb_cow_type.SelectedValue);
            sex_id = Convert.ToInt32(cb_cow_sex.SelectedValue);
            color_id = Convert.ToInt32(cb_cow_color.SelectedValue);
            birth = dt_cow_born.Value.ToString("yyyy-MM-dd");

            //Mivel ez a rész üresen is maradhat, ezért csak akkor vizsgáljuk meg regex-xel, hogyha ténylegesen tartalmaz valami karaktert
            if (error_nums == 0)
            {
                if (tb_cow_death.Text == "")
                {
                    death = "";
                    
                }
                else if (tb_cow_death.Text.Length > 0)
                {

                    regex = new Regex(@"^\d{4}-\d{1,2}-\d{1,2}$");
                    death = tb_cow_death.Text.Trim();


                    if (regex.IsMatch(death))
                    {

                    }
                    else
                    {
                        error_nums = 1;
                        global_invalid_date_form();
                        
                    }
                }
            }
           

            if (chb_cow_pregnant.Checked)
            {
                pregnant_id = 1;
            }
            else
                pregnant_id = 0;

            age_type_id = Convert.ToInt32(cb_cow_age_type.SelectedValue);

            if (error_nums == 0)
            {
                cow_update(id, mother_id, type_id, sex_id, color_id, birth, death, pregnant_id, age_type_id, prim_key);

                clear_cow_fields();

                global_success_update_box();

                show_cow_table();

                insemination_cow_id_combobox_fill();

                list_medicine_id_combobox_fill();

                list_cow_id_combobox_fill();
            }
        }

        private void dgv_cow_DoubleClick(object sender, EventArgs e)
        {
            tb_cow_id.Text = dgv_cow.CurrentRow.Cells[1].Value.ToString();
            tb_cow_mother_id.Text = dgv_cow.CurrentRow.Cells[2].Value.ToString();

            cb_cow_type.SelectedValue =dgv_cow.CurrentRow.Cells[3].Value;
            cb_cow_sex.SelectedValue = dgv_cow.CurrentRow.Cells[5].Value;
            cb_cow_color.SelectedValue = dgv_cow.CurrentRow.Cells[7].Value;
            cb_cow_age_type.SelectedValue = dgv_cow.CurrentRow.Cells[13].Value;

           
            
            dt_cow_born.Text = dgv_cow.CurrentRow.Cells[9].Value.ToString();                
            tb_cow_death.Text = dgv_cow.CurrentRow.Cells[10].Value.ToString();

            if (Convert.ToInt32(dgv_cow.CurrentRow.Cells[11].Value) == 1)
            {
                chb_cow_pregnant.CheckState = CheckState.Checked;
            }
            else
                chb_cow_pregnant.CheckState = CheckState.Unchecked;

            

        }

        private void cow_delete(int prim_key)
        {
            string sql = "DELETE FROM cow WHERE cow_primary_key = " + prim_key + "";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();
            }
        }

        private void btn_cow_delete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Biztosan törölni szeretnéd?","Törlés",MessageBoxButtons.YesNo) ==DialogResult.Yes)
            { 
                int prim_key;

                prim_key = Convert.ToInt32(dgv_cow.CurrentRow.Cells[0].Value);

                cow_delete(prim_key);

                clear_cow_fields();

                global_success_delete_box();

                show_cow_table();

                list_cow_id_combobox_fill();
            }

        }


                                                                                    //Tehenek rész vége!!!!!!!!!!!!!!!!!

                                                                                    
                                                                                    //Megtermékenyítés típusa kezdete!!!!!!!!!!!!!

        private void clear_insemination_type_fields()
        {
            tb_insemination_type_name.Text = "";
        }

        private void show_insemination_type_table()
        {
            string sql = "SELECT * FROM cow_insemination_type";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                adapter = new MySqlDataAdapter(sql, conn);

                dt = new DataTable();

                adapter.Fill(dt);

                dgv_insemination_type.DataSource = dt;
            }
        }

        private void insemination_type_insert(string name)
        {
            string sql = "INSERT INTO cow_insemination_type VALUES (null, '" + name + "')";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();
            }
        }

        private void btn_insemination_insert_Click(object sender, EventArgs e)
        {
            if (tb_insemination_type_name.Text != "")
            {
                string name;

                name = tb_insemination_type_name.Text.Trim();

                insemination_type_insert(name);

                clear_insemination_type_fields();

                global_success_insert_box();

                show_insemination_type_table();

                insemination_type_combobox_fill();
            }
            else
                global_empty_box();
        }

        private void insemination_type_update(string name, int id)
        {
            string sql = "UPDATE cow_insemination_type SET cow_insemination_type_method = '"+name+"' WHERE cow_insemination_type_id = "+id+" ";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();
            }
        }

        private void btn_insemination_update_Click(object sender, EventArgs e)
        {
            if (tb_insemination_type_name.Text != "")
            {
                string name;
                int id;

                name = tb_insemination_type_name.Text.Trim();
                id = Convert.ToInt32(dgv_insemination_type.CurrentRow.Cells[0].Value);

                insemination_type_update(name, id);

                clear_insemination_type_fields();

                global_success_update_box();

                show_insemination_type_table();

                insemination_type_combobox_fill();
            }
            else
                global_empty_box();
        }

        private void insemination_type_delete(int id)
        {
            string sql = "DELETE FROM cow_insemination_type WHERE cow_insemination_type_id = " + id + "";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();
            }
        }

        private void btn_insemination_delete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Biztosan törölni szeretnéd?","Törlés",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int id;

                id = Convert.ToInt32(dgv_insemination_type.CurrentRow.Cells[0].Value);

                insemination_type_delete(id);

                clear_insemination_type_fields();

                global_success_delete_box();

                show_insemination_type_table();

                insemination_type_combobox_fill();
            }
        }

        private void dgv_insemination_type_DoubleClick(object sender, EventArgs e)
        {
            if(dgv_insemination_type.CurrentRow.Index != -1)
            {
                tb_insemination_type_name.Text = dgv_insemination_type.CurrentRow.Cells[1].Value.ToString();
            }
        }


                                                                            //Megtermékenyítés típusa vége!!!!!!!!!!!!!


                                                                            
                                                                            //Megtermékenyítése rész kezdete!!!!!!!!!!!!!

        private void clear_insemination_fields()
        {
            dt_insemination_date.Text = "";

            cb_insemination_cow_id.SelectedItem = 0;
            cb_insemination_type.SelectedItem = 0;

            chb_insemination_successful.CheckState = CheckState.Unchecked;
        }

        private void show_insemination_table()
        {
            string sql = "SELECT cow_insemination_id, cow_insemination_date, cow_insemination_cow_id, cow_insemination_cow_insemination_type_id, cow_insemination_type_method, cow_insemination_cow_insemination_success_id, cow_insemination_success_name FROM cow_insemination INNER JOIN cow ON cow_id = cow_insemination_cow_id INNER JOIN cow_insemination_type ON cow_insemination_type_id = cow_insemination_cow_insemination_type_id INNER JOIN cow_insemination_success ON cow_insemination_cow_insemination_success_id = cow_insemination_success_id";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                adapter = new MySqlDataAdapter(sql, conn);

                dt = new DataTable();

                adapter.Fill(dt);

                dgv_insemination.DataSource = dt;
            }
        }

        private void insemination_cow_id_combobox_fill()
        {
            string sql = "SELECT cow_id FROM cow";
            ds = new DataSet();

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                adapter = new MySqlDataAdapter();

                adapter.SelectCommand = cmd;

                adapter.Fill(ds);

                cb_insemination_cow_id.DisplayMember = "cow_id";
                cb_insemination_cow_id.ValueMember = "cow_id";
                cb_insemination_cow_id.DataSource = ds.Tables[0];

            }
        }

        private void insemination_type_combobox_fill()
        {
            string sql = "SELECT * FROM cow_insemination_type";
            ds = new DataSet();

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql,conn);

                adapter = new MySqlDataAdapter();

                adapter.SelectCommand = cmd;

                adapter.Fill(ds);

                cb_insemination_type.DisplayMember = "cow_insemination_type_method";
                cb_insemination_type.ValueMember = "cow_insemination_type_id";
                cb_insemination_type.DataSource = ds.Tables[0];
              
            }
        }

        private void insemination_insert(string date, string cow_id, int insemination_type_id, int successful_id)
        {
            string sql = "INSERT INTO cow_insemination VALUES (null, '"+date+"', '"+cow_id+"', "+insemination_type_id+", "+successful_id+");";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();
            }
        }

        private void btn_insemination_insert_Click_1(object sender, EventArgs e)
        {
            
            string date;
            string cow_id;
            int insemination_type_id;
            int successful_id;

            date = dt_insemination_date.Value.ToString("yyyy-MM-dd");
            cow_id = cb_insemination_cow_id.SelectedValue.ToString();
            insemination_type_id = Convert.ToInt32(cb_insemination_type.SelectedValue);

            if (chb_insemination_successful.CheckState == CheckState.Checked)
            {
                successful_id = 1;
            }
            else
                successful_id = 0;

            insemination_insert(date, cow_id, insemination_type_id, successful_id);

            clear_insemination_fields();

            global_success_insert_box();

            show_insemination_table();

        }

        private void insemination_update(string date, string cow_id, int insemination_type_id, int successful_id, int id)
        {
            string sql = "UPDATE cow_insemination SET cow_insemination_date = '"+date+"', cow_insemination_cow_id = '"+cow_id+ "', cow_insemination_cow_insemination_type_id = "+insemination_type_id+ ", cow_insemination_cow_insemination_success_id = "+ successful_id+ " WHERE cow_insemination_id = " + id+";";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql,conn);

                cmd.ExecuteNonQuery();

            }
        }

        private void btn_insemination_update_Click_1(object sender, EventArgs e)
        {
            string date;
            string cow_id;
            int insemination_type_id;
            int successful_id;
            int id;

            date = dt_insemination_date.Value.ToString("yyyy_MM-dd");
            cow_id = cb_insemination_cow_id.SelectedValue.ToString();
            insemination_type_id = Convert.ToInt32(cb_insemination_type.SelectedValue);

            if (chb_insemination_successful.CheckState == CheckState.Checked)
            {
                successful_id = 1;
            }
            else
                successful_id = 0;

            id = Convert.ToInt32(dgv_insemination.CurrentRow.Cells[0].Value);

            insemination_update(date, cow_id, insemination_type_id, successful_id, id);

            clear_insemination_fields();

            global_success_update_box();

            show_insemination_table();
             
        }

        private void insemination_delete(int id)
        {
            string sql = "DELETE FROM cow_insemination WHERE cow_insemination_id = "+id+"";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();
            }
        }

        private void btn_insemination_delete_Click_1(object sender, EventArgs e)
        {
            int id;

            id = Convert.ToInt32(dgv_insemination.CurrentRow.Cells[0].Value);

            insemination_delete(id);

            clear_insemination_fields();

            global_success_delete_box();

            show_insemination_table();
        }

        private void dgv_insemination_DoubleClick(object sender, EventArgs e)
        {
            dt_insemination_date.Text = dgv_insemination.CurrentRow.Cells[1].Value.ToString();
            

            cb_insemination_cow_id.SelectedValue = dgv_insemination.CurrentRow.Cells[2].Value;
            cb_insemination_type.SelectedValue = dgv_insemination.CurrentRow.Cells[3].Value;

            if (Convert.ToInt32(dgv_insemination.CurrentRow.Cells[5].Value) == 1)
            {
                chb_insemination_successful.CheckState = CheckState.Checked;
            }
            else
                chb_insemination_successful.CheckState = CheckState.Unchecked;
        }

                                                                                //Megtermékenyítés rész vége!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!



                                                                                //Gyógyszeres kezelések kezdete!!!!!!!!!!!!!!!!!!!!!!!!!!








        private void clear_list_fields()
        {
            cb_list_cow_id.SelectedIndex = 0;
            cb_list_medicine_id.SelectedIndex = 0;

            dt_list_med_start.ResetText();

            dt_list_med_end.ResetText();

            rtb_list_misc.ResetText();
        }

        private void show_list_table()
        {
            string sql = "SELECT list_id, list_cow_id, list_medicine_id, medicine_name, medicine_type_name, list_medicine_start, list_medicine_expiry, list_medicine_dosage_method FROM list INNER JOIN medicine ON list_medicine_id = medicine_id INNER JOIN cow ON list_cow_id = cow_id INNER JOIN medicine_type ON medicine_type_id = medicine_medicine_type_id";


            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                adapter = new MySqlDataAdapter(sql, conn);

                dt = new DataTable();

                adapter.Fill(dt);

                dgv_list.DataSource = dt;
            }
        }

        private void list_cow_id_combobox_fill()
        {
            string sql = "SELECT cow_id FROM cow";
            ds = new DataSet();

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                adapter = new MySqlDataAdapter();

                adapter.SelectCommand = cmd;

                adapter.Fill(ds);

                cb_list_cow_id.DisplayMember = "cow_id";
                cb_list_cow_id.ValueMember = "cow_id";
                cb_list_cow_id.DataSource = ds.Tables[0];
     
            }
        }

        private void list_medicine_id_combobox_fill()
        {
            string sql = "SELECT medicine_id,medicine_name FROM medicine";
            ds = new DataSet();

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                adapter = new MySqlDataAdapter();

                adapter.SelectCommand = cmd;

                adapter.Fill(ds);

                cb_list_medicine_id.DisplayMember = "medicine_name";
                cb_list_medicine_id.ValueMember = "medicine_id";
                cb_list_medicine_id.DataSource = ds.Tables[0];
            }
        }

        private void list_insert(string cow_id, int medicine_id, string medicine_start, string medicine_expiry, string misc)
        {
            string sql = "INSERT INTO list VALUES (null, '"+cow_id+"', "+medicine_id+", '"+medicine_start+"', '"+medicine_expiry+"', '"+misc+"')";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();
            }

        }

        private void btn_list_insert_Click(object sender, EventArgs e)
        {
            string cow_id;
            int medicine_id;
            string medicine_start;
            string medicine_expiry;
            string misc;


            cow_id = cb_list_cow_id.SelectedValue.ToString();
            medicine_id = Convert.ToInt32(cb_list_medicine_id.SelectedValue);
            medicine_start = dt_list_med_start.Value.ToString("yyyy-MM-dd");
            medicine_expiry = dt_list_med_end.Value.ToString("yyyy-MM-dd");
            misc = rtb_list_misc.Text;
           
            list_insert(cow_id, medicine_id, medicine_start, medicine_expiry, misc);

            clear_list_fields();

            global_success_insert_box();
        
            show_list_table();
           
        }

        private void list_update(string cow_id, int medicine_id, string medicine_start, string medicine_expiry, string misc, int list_id)
        {
            string sql = "UPDATE list SET list_cow_id = "+cow_id+ ", list_medicine_id = "+medicine_id+ ", list_medicine_start = '"+medicine_start+ "', list_medicine_expiry = '"+medicine_expiry+ "', list_medicine_dosage_method = '"+misc+"' WHERE list_id = "+list_id+" ";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql, conn);

                cmd.ExecuteNonQuery();
            }
        }

        private void btn_list_update_Click(object sender, EventArgs e)
        {
            string cow_id;
            int medicine_id;
            string medicine_start;
            string medicine_expiry;
            string misc;
            int list_id;

            cow_id = cb_list_cow_id.SelectedValue.ToString();
            medicine_id = Convert.ToInt32(cb_list_medicine_id.SelectedValue);
            medicine_start = dt_list_med_start.Value.ToString("yyyy-MM-dd");
            medicine_expiry = dt_list_med_end.Value.ToString("yyyy-MM-dd");
            misc = rtb_list_misc.Text;
            list_id = Convert.ToInt32(dgv_list.CurrentRow.Cells[0].Value);

            list_update(cow_id, medicine_id, medicine_start, medicine_expiry, misc, list_id);

            clear_list_fields();

            global_success_update_box();

            show_list_table();
        }

        private void list_delete(int id)
        {
            string sql = "DELETE from list WHERE list_id = " + id + "";

            using (conn = new MySqlConnection(connection_string))
            {
                conn.Open();

                cmd = new MySqlCommand(sql,conn);

                cmd.ExecuteNonQuery();
            }
        }

        private void btn_list_delete_Click(object sender, EventArgs e)
        {
            int id;

            id = Convert.ToInt32(dgv_list.CurrentRow.Cells[0].Value);

            if(MessageBox.Show("Biztosan törölni szeretnéd?", "Törlés",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                list_delete(id);

                clear_list_fields();

                global_success_delete_box();

                show_list_table();
            }


        }

        private void dgv_list_DoubleClick(object sender, EventArgs e)
        {
            cb_list_cow_id.SelectedValue = dgv_list.CurrentRow.Cells[1].Value;
            cb_list_medicine_id.SelectedValue = dgv_list.CurrentRow.Cells[2].Value;

            dt_list_med_start.Text = dgv_list.CurrentRow.Cells[5].Value.ToString();
            dt_list_med_end.Text = dgv_list.CurrentRow.Cells[6].Value.ToString();

            rtb_list_misc.Text = dgv_list.CurrentRow.Cells[7].Value.ToString();
        }
    }


                                                                                    //Gyógyszeres kezelések kezdete!!!!!!!!!!!!!!!!!!!!!!!!!!

}

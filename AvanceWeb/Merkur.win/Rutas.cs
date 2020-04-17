using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace Merkur.win
{
    public partial class Rutas : Form
    {
        
        public string Ladinicio { get; set; }
        public string Loginicio { get; set; }
        public string Ladifinal { get; set; }
        public string Logifinal { get; set; }
        GMarkerGoogle marker;
        GMapOverlay markerOverlay;
        DataTable dt;

        //como llegar
        bool trazarRuta = false;
        int IndicadoresdeRuta = 0;
        PointLatLng inicio;
        PointLatLng final;

        int filaseleccionada = 0;
        double LatInicial = 15.5041704;
        double LngInicial = -88.0250015;
        public Rutas()
         
        {
            InitializeComponent();
        }
        public Rutas(string lat1,string lat2, string log1, string log2)

        {
            Ladinicio = lat1;
            Ladifinal = lat2;
            Loginicio = log1;
            Logifinal = log2;

            InitializeComponent();

            
        }


        private void Rutas_Load_1(object sender, EventArgs e)
        {        
            dt = new DataTable();
            dt.Columns.Add(new DataColumn("Descripcion", typeof(string)));
            dt.Columns.Add(new DataColumn("Latitud", typeof(double)));
            dt.Columns.Add(new DataColumn("Longitud", typeof(double)));

            //insertamos datos al dt
            dt.Rows.Add("Ubicacion 1", LatInicial, LngInicial);
            dataGridView1.DataSource = dt;

            //escondemos la longitud y la latitud
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;

            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(LatInicial, LngInicial);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 9;
            gMapControl1.AutoScroll = true;

            //marcador
            markerOverlay = new GMapOverlay("Marcador");
            marker = new GMarkerGoogle(new PointLatLng(LatInicial, LngInicial), GMarkerGoogleType.red);
            markerOverlay.Markers.Add(marker);//agregamos al Mapa

            //texto a los marcadores
            marker.ToolTipMode = MarkerTooltipMode.Always;
            marker.ToolTipText = string.Format("Ubicacion: \n Latitud:{0} \n Longitud:{1}", LatInicial, LngInicial);

            //agregamos al mapa
            gMapControl1.Overlays.Add(markerOverlay);
        }

        private void SeleccionarRegistro(object sender, DataGridViewCellMouseEventArgs e)
        {
            filaseleccionada = e.RowIndex;
            txtdescripcion.Text = dataGridView1.Rows[filaseleccionada].Cells[0].Value.ToString();
            txtlatitud.Text = dataGridView1.Rows[filaseleccionada].Cells[1].Value.ToString();
            txtlongitud.Text = dataGridView1.Rows[filaseleccionada].Cells[2].Value.ToString();

            marker.Position = new PointLatLng(Convert.ToDouble(txtlatitud.Text), Convert.ToDouble(txtlongitud.Text));
            gMapControl1.Position = marker.Position;
        }

        private void gMapControl1_DoubleClick(object sender, EventArgs ev)
        {
            var e = ev as MouseEventArgs;

            double lat = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
            double lng = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;


            //colocamos la longitud y latitud en los txt
            txtlatitud.Text = lat.ToString();
            txtlongitud.Text = lng.ToString();
            //movilidad del marcador
            marker.Position = new PointLatLng(lat, lng);
            //agregamos el texto a la burbuja
            marker.ToolTipText = string.Format("Ubicacion: \n Ltitud: {0} \n Longitud:{1}", lat, lng);

            CrearDireccionTrazarRuta(lat, lng);
        }

        private void gMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            double lat = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
            double lng = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;


            //colocamos la longitud y latitud en los txt
            txtlatitud.Text = lat.ToString();
            txtlongitud.Text = lng.ToString();
            //movilidad del marcador
            marker.Position = new PointLatLng(lat, lng);
            //agregamos el texto a la burbuja
            marker.ToolTipText = string.Format("Ubicacion: \n Ltitud: {0} \n Longitud:{1}", lat, lng);

            CrearDireccionTrazarRuta(lat, lng);

        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            dt.Rows.Add(txtdescripcion.Text, txtlatitud.Text, txtlongitud.Text);
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(filaseleccionada);

        }

        private void btnpoligono_Click(object sender, EventArgs e)
        {
            GMapOverlay Poligono = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            double lng, lat;
            for (int filas = 0; filas < dataGridView1.Rows.Count; filas++)
            {
                lat = Convert.ToDouble(dataGridView1.Rows[filas].Cells[1].Value);
                lng = Convert.ToDouble(dataGridView1.Rows[filas].Cells[2].Value);
                puntos.Add(new PointLatLng(lat, lng));
            }
            GMapPolygon poligonoPuntos = new GMapPolygon(puntos, "Poligono");
            Poligono.Polygons.Add(poligonoPuntos);
            gMapControl1.Overlays.Add(Poligono);
            gMapControl1.Zoom = gMapControl1.Zoom + 1;
            gMapControl1.Zoom = gMapControl1.Zoom - 1;
        }

        private void btnruta_Click(object sender, EventArgs e)
        {
            GMapOverlay Ruta = new GMapOverlay("CapaRuta");
            List<PointLatLng> puntos = new List<PointLatLng>();
            double lng, lat;
            for (int filas = 0; filas < dataGridView1.Rows.Count; filas++)
            {
                lat = Convert.ToDouble(dataGridView1.Rows[filas].Cells[1].Value);
                lng = Convert.ToDouble(dataGridView1.Rows[filas].Cells[2].Value);
                puntos.Add(new PointLatLng(lat, lng));
            }
            GMapRoute PuntosRuta = new GMapRoute(puntos, "Ruta");
            Ruta.Routes.Add(PuntosRuta);
            gMapControl1.Overlays.Add(Ruta);
            gMapControl1.Zoom = gMapControl1.Zoom + 1;
            gMapControl1.Zoom = gMapControl1.Zoom - 1;
        }

        private void btnllegar_Click(object sender, EventArgs e)
        {
            trazarRuta = true;
            btnllegar.Enabled = false;
        }

        public void CrearDireccionTrazarRuta(double lat, double lng)
        {
            if (trazarRuta)
            {
                switch (IndicadoresdeRuta)
                {
                    case 0: //punto inicio
                        IndicadoresdeRuta++;
                        inicio = new PointLatLng(lat, lng);
                        
                        break;
                    case 1: //punto final
                        IndicadoresdeRuta++;
                        final = new PointLatLng(lat, lng);
                        GDirections direccion;

                        GMapProviders.GoogleMap.ApiKey = "AIzaSyBsgEjD9rVUutTlcmnuDqnfpLPesMY-Ztg";

                        var RutasDireccion = GMapProviders.GoogleMap.GetDirections(out direccion, inicio, final, false, false, false, false, false);
                        GMapRoute RutaObtenida = new GMapRoute(direccion.Route, "Ruta ubicacion");
                        GMapOverlay CapaRutas = new GMapOverlay("Capa de la Ruta");
                        CapaRutas.Routes.Add(RutaObtenida);
                        gMapControl1.Overlays.Add(CapaRutas);
                        gMapControl1.Zoom = gMapControl1.Zoom + 1;
                        gMapControl1.Zoom = gMapControl1.Zoom - 1;
                        IndicadoresdeRuta = 0;
                        trazarRuta = false;
                        btnllegar.Enabled = true;
                        break;
                }
            }
        }

     
        private void btnSatelite_Click_1(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleSatelliteMap;
        }   

        private void btnRelieve_Click_1(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleTerrainMap;
        }

        private void btnOriginal_Click_1(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            trackBar1.Value = Convert.ToInt32(gMapControl1.Zoom);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            gMapControl1.Zoom = trackBar1.Value;
        }

        private void btnagregar_Click_1(object sender, EventArgs e)
        {
            dt.Rows.Add(txtdescripcion.Text, txtlatitud.Text, txtlongitud.Text);
        }

        private void btneliminar_Click_1(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(filaseleccionada);
        }

        private void btnpoligono_Click_1(object sender, EventArgs e)
        {
            GMapOverlay Poligono = new GMapOverlay("Poligono");
            List<PointLatLng> puntos = new List<PointLatLng>();
            double lng, lat;
            for (int filas = 0; filas < dataGridView1.Rows.Count; filas++)
            {
                lat = Convert.ToDouble(dataGridView1.Rows[filas].Cells[1].Value);
                lng = Convert.ToDouble(dataGridView1.Rows[filas].Cells[2].Value);
                puntos.Add(new PointLatLng(lat, lng));
            }
            GMapPolygon poligonoPuntos = new GMapPolygon(puntos, "Poligono");
            Poligono.Polygons.Add(poligonoPuntos);
            gMapControl1.Overlays.Add(Poligono);
            gMapControl1.Zoom = gMapControl1.Zoom + 1;
            gMapControl1.Zoom = gMapControl1.Zoom - 1;
        }

        private void btnruta_Click_1(object sender, EventArgs e)
        {
            GMapOverlay Ruta = new GMapOverlay("CapaRuta");
            List<PointLatLng> puntos = new List<PointLatLng>();
            double lng, lat;
            for (int filas = 0; filas < dataGridView1.Rows.Count; filas++)
            {
                lat = Convert.ToDouble(dataGridView1.Rows[filas].Cells[1].Value);
                lng = Convert.ToDouble(dataGridView1.Rows[filas].Cells[2].Value);
                puntos.Add(new PointLatLng(lat, lng));
            }
            GMapRoute PuntosRuta = new GMapRoute(puntos, "Ruta");
            Ruta.Routes.Add(PuntosRuta);
            gMapControl1.Overlays.Add(Ruta);
            gMapControl1.Zoom = gMapControl1.Zoom + 1;
            gMapControl1.Zoom = gMapControl1.Zoom - 1;
        }

        private void btnllegar_Click_1(object sender, EventArgs e)
        {
            trazarRuta = true;
            btnllegar.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Productos I = new Productos();
            //I.longitud1TextBox.Text = txtlongitud.Text;
            //I.latitud1TextBox.Text = txtlatitud.Text;
            Loginicio = txtlongitud.Text;
            Ladinicio = txtlatitud.Text;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Productos F = new Productos();
            //F.longitud2TextBox.Text = txtlongitud.Text;
            //F.latitud2TextBox.Text = txtlatitud.Text;
            Logifinal = txtlongitud.Text;
            Ladifinal = txtlatitud.Text;
            this.Close();
        }

       
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Merkur.BL
{
    public class DatosdeIncio : CreateDatabaseIfNotExists<Contexto>
    {
        protected override void Seed(Contexto contexto)
        {

            //Usuarios
            var User2 = new Usuarios();
            User2.Nombre = "Ronaldo";
            User2.Contrasena = Encriptar.CodificarContrasena("325");
            User2.Privilegio = "Administrador";
            contexto.Usuarios.Add(User2);

            var Admin = new Usuarios();
            Admin.Nombre = "Admin";
            Admin.Contrasena = Encriptar.CodificarContrasena("123");
            Admin.Privilegio = "Administrador";
            contexto.Usuarios.Add(Admin);

            var User = new Usuarios();
            User.Nombre = "Jeffry";
            User.Contrasena = Encriptar.CodificarContrasena("456");
            User.Privilegio = "Usuario";
            contexto.Usuarios.Add(User);

            var User1 = new Usuarios();
            User1.Nombre = "Orlando";
            User1.Contrasena = Encriptar.CodificarContrasena("789");
            User1.Privilegio = "Usuario1";
            contexto.Usuarios.Add(User1);

            //Categorias

            var categoria1 = new Categoria();
            categoria1.Descripcion = "Fragil";
            contexto.Categorias.Add(categoria1);

            var categoria2 = new Categoria();
            categoria2.Descripcion = "Pesado";
            contexto.Categorias.Add(categoria2);

            var categoria3 = new Categoria();
            categoria3.Descripcion = "Documentacion";
            contexto.Categorias.Add(categoria3);

            var categoria4 = new Categoria();
            categoria4.Descripcion = "Peligroso";
            contexto.Categorias.Add(categoria4);

            //Tipos de envios

            var tipo1 = new Tipo();
            tipo1.Descripcion = "Urgente";
            contexto.Tipo.Add(tipo1);

            var tipo2 = new Tipo();
            tipo2.Descripcion = "No Urgente";
            contexto.Tipo.Add(tipo2);

            //Clientes

            var clientes = new Cliente();
            
            clientes.Nombres = "Orlando";
            clientes.Apellidos = "Garcia";
            clientes.Cedula = "0502-1999-01495";
            clientes.Email = "FuentesGarcia197@gmail.com";
            clientes.Telefono = "97681994";         
            contexto.Cliente.Add(clientes);


            var clientes2 = new Cliente();
            
            clientes2.Nombres = "Ronaldo";
            clientes2.Apellidos = "Zelaya";
            clientes2.Cedula = "1602-1997-01458";
            clientes2.Email = "FreddyZelaya@gmail.com";
            clientes2.Telefono = "95658745";
            contexto.Cliente.Add(clientes2);

            //paquetes(productos)

            var producto1 = new Producto();
            producto1.Activo = true;
            producto1.Descripcion = "paquete1";
            producto1.Destino = "Colón";
            producto1.Tipo = tipo1;
            producto1.Categorias = categoria3;
            producto1.FechadeEntrega = DateTime.Now;
            producto1.Precio = 150;
            contexto.Productos.Add(producto1);

            //Ventas
            var venta1 = new venta();
            venta1.Descripcion = "Envio Grande";
            venta1.Activo = true;
            contexto.Ventas.Add(venta1);

            var venta2 = new venta();
            venta2.Descripcion = "Envio Mediano";
            venta2.Activo = true;
            contexto.Ventas.Add(venta2);

            var venta3 = new venta();
            venta3.Descripcion = "Envio Pequeño";
            venta3.Activo = true;
            contexto.Ventas.Add(venta3);









            base.Seed(contexto);
        }
    }
}

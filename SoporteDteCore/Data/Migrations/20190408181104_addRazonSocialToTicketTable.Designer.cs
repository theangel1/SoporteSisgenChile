// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SoporteDteCore.Data;

namespace SoporteDteCore.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190408181104_addRazonSocialToTicketTable")]
    partial class addRazonSocialToTicketTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SoporteDteCore.Models.Bloqueos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FechaBloqueo");

                    b.Property<bool>("IsSuspended");

                    b.Property<string>("Observacion");

                    b.Property<string>("RazonSocial")
                        .IsRequired();

                    b.Property<string>("Rut")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Bloqueos");
                });

            modelBuilder.Entity("SoporteDteCore.Models.BoletaElectronica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Estado");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<string>("Observacion");

                    b.Property<string>("RazonSocial")
                        .IsRequired();

                    b.Property<string>("RutCliente")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("BoletaElectronica");
                });

            modelBuilder.Entity("SoporteDteCore.Models.DetalleTicket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Detalle")
                        .IsRequired();

                    b.Property<int>("TicketId");

                    b.Property<DateTime>("TimeStamp");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.ToTable("DetalleTickets");
                });

            modelBuilder.Entity("SoporteDteCore.Models.EnvioCarta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("FechaEnvioCarta1");

                    b.Property<DateTime?>("FechaEnvioCarta2");

                    b.Property<DateTime?>("FechaEnvioCarta3");

                    b.Property<DateTime>("FechaMorosidad");

                    b.Property<double>("Morosidad");

                    b.Property<string>("RazonSocial")
                        .IsRequired();

                    b.Property<string>("RutCliente")
                        .IsRequired();

                    b.Property<double>("SinAtraso");

                    b.Property<double>("TotalDeuda");

                    b.HasKey("Id");

                    b.ToTable("EnvioCartas");
                });

            modelBuilder.Entity("SoporteDteCore.Models.FacturaElectronica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Estado");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<string>("Observacion");

                    b.Property<string>("RazonSocial")
                        .IsRequired();

                    b.Property<string>("RutCliente")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("FacturaElectronica");
                });

            modelBuilder.Entity("SoporteDteCore.Models.FacturaExportacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Estado");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<string>("Observacion");

                    b.Property<string>("RazonSocial")
                        .IsRequired();

                    b.Property<string>("RutCliente")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("FacturaExportacion");
                });

            modelBuilder.Entity("SoporteDteCore.Models.InternalWorkOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .IsRequired();

                    b.Property<DateTime>("FechaInicio");

                    b.Property<DateTime>("FechaTermino");

                    b.Property<bool>("IsValid");

                    b.Property<string>("NombreCliente")
                        .IsRequired();

                    b.Property<string>("Observacion");

                    b.Property<string>("RutCliente")
                        .IsRequired();

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("InternalWorkOrder");
                });

            modelBuilder.Entity("SoporteDteCore.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Conexion");

                    b.Property<DateTime>("FechaCierre");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<bool>("IsActive");

                    b.Property<string>("NombreTecnico");

                    b.Property<string>("Problema");

                    b.Property<string>("RazonSocial")
                        .IsRequired();

                    b.Property<string>("RutCliente")
                        .IsRequired();

                    b.Property<string>("TipoEstacion");

                    b.HasKey("Id");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("SoporteDteCore.Models.Visita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EstadoActual");

                    b.Property<string>("EstadoFinal");

                    b.Property<DateTime>("FechaProgramacion");

                    b.Property<bool>("IsActive");

                    b.Property<string>("MotivoVisita");

                    b.Property<string>("NombreContacto");

                    b.Property<string>("NombreTecnico");

                    b.Property<string>("NumeroContacto");

                    b.Property<string>("Observacion");

                    b.Property<string>("RazonSocial");

                    b.Property<string>("RutCliente");

                    b.HasKey("Id");

                    b.ToTable("Visita");
                });

            modelBuilder.Entity("SoporteDteCore.Models.WorkOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Derivado");

                    b.Property<DateTime>("HoraLlegada");

                    b.Property<DateTime>("HoraSalida");

                    b.Property<bool>("IsActive");

                    b.Property<string>("MotivoVisita");

                    b.Property<string>("NombreEmpresa");

                    b.Property<string>("NombreImagenFirma");

                    b.Property<string>("NombrePersonaRecibe");

                    b.Property<string>("NombreTecnico");

                    b.Property<string>("Observaciones");

                    b.Property<string>("RutEmpresa");

                    b.Property<string>("TrabajoRealizado");

                    b.Property<int?>("VisitaId");

                    b.HasKey("Id");

                    b.HasIndex("VisitaId");

                    b.ToTable("WorkOrders");
                });

            modelBuilder.Entity("SoporteDteCore.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SoporteDteCore.Models.DetalleTicket", b =>
                {
                    b.HasOne("SoporteDteCore.Models.Ticket", "Ticket")
                        .WithMany()
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SoporteDteCore.Models.InternalWorkOrder", b =>
                {
                    b.HasOne("SoporteDteCore.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SoporteDteCore.Models.WorkOrder", b =>
                {
                    b.HasOne("SoporteDteCore.Models.Visita", "Visita")
                        .WithMany()
                        .HasForeignKey("VisitaId");
                });
#pragma warning restore 612, 618
        }
    }
}

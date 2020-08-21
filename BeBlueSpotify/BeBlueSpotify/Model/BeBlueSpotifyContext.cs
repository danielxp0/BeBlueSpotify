using System;
using BeBlueSpotify.Arquitetura.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace BeBlueSpotify.Model
{
    public partial class BeBlueSpotifyContext : DbContext
    {
        public BeBlueSpotifyContext()
        {
        }

        public BeBlueSpotifyContext(DbContextOptions<BeBlueSpotifyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Album> Album { get; set; }
        public virtual DbSet<CashSemanal> CashSemanal { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Venda> Venda { get; set; }
        public virtual DbSet<VendaItem> VendaItem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuracao.GetConfiguracao().GetConnectionString("BeBlue"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasKey(e => e.Idalbum);

                entity.Property(e => e.Idalbum).HasColumnName("IDAlbum");

                entity.Property(e => e.Artista)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Idgenero).HasColumnName("IDGenero");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdgeneroNavigation)
                    .WithMany(p => p.Album)
                    .HasForeignKey(d => d.Idgenero)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Album_Genero");
            });

            modelBuilder.Entity<CashSemanal>(entity =>
            {
                entity.HasKey(e => e.IdcashSemanal);

                entity.Property(e => e.IdcashSemanal).HasColumnName("IDCashSemanal");

                entity.Property(e => e.Cash).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Idgenero).HasColumnName("IDGenero");

                entity.HasOne(d => d.IdgeneroNavigation)
                    .WithMany(p => p.CashSemanal)
                    .HasForeignKey(d => d.Idgenero)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CashSemanal_Genero");
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.HasKey(e => e.Idgenero);

                entity.Property(e => e.Idgenero).HasColumnName("IDGenero");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Idusuario);

                entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");

                entity.Property(e => e.CashAcumulado).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Venda>(entity =>
            {
                entity.HasKey(e => e.Idvenda);

                entity.Property(e => e.Idvenda).HasColumnName("IDVenda");

                entity.Property(e => e.Data).HasColumnType("datetime");

                entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");

                entity.HasOne(d => d.IdusuarioNavigation)
                    .WithMany(p => p.Venda)
                    .HasForeignKey(d => d.Idusuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Venda_Usuario");
            });

            modelBuilder.Entity<VendaItem>(entity =>
            {
                entity.HasKey(e => e.IdvendaItem);

                entity.Property(e => e.IdvendaItem).HasColumnName("IDVendaItem");

                entity.Property(e => e.Cash).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Idalbum).HasColumnName("IDAlbum");

                entity.Property(e => e.Idvenda).HasColumnName("IDVenda");

                entity.HasOne(d => d.IdalbumNavigation)
                    .WithMany(p => p.VendaItem)
                    .HasForeignKey(d => d.Idalbum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VendaItem_Album");

                entity.HasOne(d => d.IdvendaNavigation)
                    .WithMany(p => p.VendaItem)
                    .HasForeignKey(d => d.Idvenda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VendaItem_Venda");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public partial class saulius_nobedscomEntities : DbContext
    {
        public virtual DbSet<accounting> Accounting { get; set; }
        public virtual DbSet<availability> availability { get; set; }
        public virtual DbSet<hotel> hotel { get; set; }
        public virtual DbSet<review> review { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<rentals> rentals { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=185.193.27.93;Database=SQL-MGDLMQMT9KG;initial catalog=saulius_nobeds_app;persist security info=True;user id=WIN-MGDLMQMT9KG;password=D)C&Hp5/5&q./?HQ;Trusted_Connection=True;Connect Timeout = 30; pooling = 'true'; Max Pool Size = 200;Max Pool Size=500; Integrated security=false; MultipleActiveResultSets=true");//MultipleActiveResultSets=true
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<accounting>(entity =>
            {
                entity.HasKey(e => e.rid);

                entity.ToTable("accounting");

                entity.Property(e => e.rid).HasColumnName("rid");

                entity.Property(e => e.@checked).HasColumnName("checked");

                entity.Property(e => e.comment)
                    .HasColumnName("comment")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.hotel_id).HasColumnName("hotel_id");

                entity.Property(e => e.manager_id).HasColumnName("manager_id");

                entity.Property(e => e.order_id).HasColumnName("order_id");

                entity.Property(e => e.room_id).HasColumnName("room_id");

                entity.Property(e => e.timestamp).HasColumnName("timestamp");

                entity.Property(e => e.title)
                    .HasColumnName("title")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.value).HasColumnName("value");
            });

           

            modelBuilder.Entity<availability>(entity =>
            {
                entity.HasKey(e => e.availability_id);

                entity.ToTable("availability");

                entity.Property(e => e.availability_id).HasColumnName("availability_id");

                entity.Property(e => e.comment).HasColumnName("comment");

                entity.Property(e => e.created)
                    .HasColumnName("created")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.hotel_id).HasColumnName("hotel_id");

                entity.Property(e => e.price).HasColumnName("price");

                entity.Property(e => e.quantity).HasColumnName("quantity");

                entity.Property(e => e.room_id).HasColumnName("room_id");

                entity.Property(e => e.time)
                    .HasColumnName("time")
                    .HasColumnType("time(0)");

                entity.Property(e => e.updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.user_id).HasColumnName("user_id");
            });

            modelBuilder.Entity<hotel>(entity =>
            {
                entity.HasKey(e => e.hotel_id);

                entity.ToTable("hotel");

                entity.HasIndex(e => e.hotel_id)
                    .HasName("hotel_id");

                entity.Property(e => e.hotel_id).HasColumnName("hotel_id");

                entity.Property(e => e.address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(255);

                entity.Property(e => e.approval_text)
                    .HasColumnName("approval_text")
                    .HasColumnType("text");

                entity.Property(e => e.city)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(100);

                entity.Property(e => e.company_details).HasColumnName("company_details");

                entity.Property(e => e.coordinates)
                    .HasColumnName("coordinates")
                    .HasMaxLength(255);

                entity.Property(e => e.country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasMaxLength(100);

                entity.Property(e => e.created)
                    .HasColumnName("created")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.currencies)
                    .HasColumnName("currencies")
                    .HasMaxLength(255);

                entity.Property(e => e.deleted)
                    .HasColumnName("deleted")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.description).HasColumnName("description");

                entity.Property(e => e.direct_discount).HasColumnName("direct_discount");

                entity.Property(e => e.direct_link).HasColumnName("direct_link");

                entity.Property(e => e.email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.invoice_currency)
                    .HasColumnName("invoice_currency")
                    .HasMaxLength(10);

                entity.Property(e => e.invoice_footer).HasColumnName("invoice_footer");

                entity.Property(e => e.invoice_prefix)
                    .HasColumnName("invoice_prefix")
                    .HasMaxLength(10);

                entity.Property(e => e.invoice_text).HasColumnName("invoice_text");

                entity.Property(e => e.languages)
                    .HasColumnName("languages")
                    .HasMaxLength(255);

                entity.Property(e => e.last_login)
                    .HasColumnName("last_login")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.logo)
                    .HasColumnName("logo")
                    .HasMaxLength(300);

                entity.Property(e => e.other_info).HasColumnName("other_info");

                entity.Property(e => e.phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(45);

                entity.Property(e => e.postal_code)
                    .IsRequired()
                    .HasColumnName("postal_code")
                    .HasMaxLength(10);

                entity.Property(e => e.promo_discount).HasColumnName("promo_discount");

                entity.Property(e => e.review_link).HasColumnName("review_link");

                entity.Property(e => e.signature)
                    .HasColumnName("signature")
                    .HasColumnType("text");

                entity.Property(e => e.terms_conditions)
                    .HasColumnName("terms_conditions")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(255);

                entity.Property(e => e.type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(64);

                entity.Property(e => e.vat).HasColumnName("vat");
            });

            modelBuilder.Entity<order>(entity =>
            {
                entity.HasKey(e => e.order_id);

                entity.ToTable("orders");

                entity.Property(e => e.order_id).HasColumnName("order_id");

                entity.Property(e => e.address)
                    .HasColumnName("address")
                    .HasMaxLength(255);

                entity.Property(e => e.balance).HasColumnName("balance");

                entity.Property(e => e.Canceled)
                    .HasColumnName("Canceled")
                    .HasColumnType("datetime");

                entity.Property(e => e.checkin)
                    .HasColumnName("checkin")
                    .HasColumnType("datetime");

                entity.Property(e => e.checkout)
                    .HasColumnName("checkout")
                    .HasColumnType("datetime");

                entity.Property(e => e.comment).HasColumnName("comission");

                entity.Property(e => e.comment).HasColumnName("comment");

                entity.Property(e => e.country)
                    .HasColumnName("country")
                    .HasMaxLength(255);

                entity.Property(e => e.created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.deleted)
                    .HasColumnName("deleted")
                    .HasColumnType("datetime");

                entity.Property(e => e.email)
                    .HasColumnName("email")
                    .HasMaxLength(255);

                entity.Property(e => e.genius)
                    .HasColumnName("genius")
                    .HasMaxLength(50);

                entity.Property(e => e.guests).HasColumnName("guests");

                entity.Property(e => e.hotel_id).HasColumnName("hotel_id");

                entity.Property(e => e.inserted)
                    .HasColumnName("inserted")
                    .HasColumnType("datetime");

                entity.Property(e => e.invoice).HasColumnName("invoice");

                entity.Property(e => e.name)
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.nights).HasColumnName("nights");

                entity.Property(e => e.phone)
                    .HasColumnName("phone")
                    .HasMaxLength(100);

                entity.Property(e => e.prepay).HasColumnName("prepay");

                entity.Property(e => e.price).HasColumnName("price");

                entity.Property(e => e.rating).HasColumnName("rating");

                entity.Property(e => e.referral)
                    .HasColumnName("referral")
                    .HasMaxLength(100);

                entity.Property(e => e.referral_order_id)
                    .HasColumnName("referral_order_id")
                    .HasMaxLength(100);

                entity.Property(e => e.room_id).HasColumnName("room_id");

                entity.Property(e => e.roomreservation_id)
                    .HasColumnName("roomreservation_id")
                    .HasMaxLength(100);

                entity.Property(e => e.ruid).HasColumnName("ruid");

                entity.Property(e => e.staff)
                    .HasColumnName("staff")
                    .HasMaxLength(50);

                entity.Property(e => e.status)
                    .HasColumnName("status")
                    .HasMaxLength(50);

                entity.Property(e => e.total).HasColumnName("total");

                entity.Property(e => e.updated)
                    .HasColumnName("updated")
                    .HasColumnType("datetime");

                entity.Property(e => e.user_id).HasColumnName("user_id");
            });
        }
    }
}
public class AcaiDeliveryDbContext : DbContext
{
    public AcaiDeliveryDbContext(DbContextOptions<AcaiDeliveryDbContext> options) : base(options) { }
    
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Estabelecimento> Estabelecimentos { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Adicional> Adicionais { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<ItemPedido> ItensPedido { get; set; }
    public DbSet<ItemPedidoAdicional> ItensPedidoAdicionais { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Usuario>()
            .ToTable("Usuarios")
            .HasDiscriminator<string>("TipoUsuario")
            .HasValue<Cliente>("Cliente")
            .HasValue<Estabelecimento>("Estabelecimento");
            
        modelBuilder.Entity<Produto>()
            .HasOne(p => p.Estabelecimento)
            .WithMany(e => e.Produtos)
            .HasForeignKey(p => p.EstabelecimentoId);
            
        modelBuilder.Entity<Adicional>()
            .HasOne(a => a.Estabelecimento)
            .WithMany()
            .HasForeignKey(a => a.EstabelecimentoId);
            
        modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Cliente)
            .WithMany(c => c.Pedidos)
            .HasForeignKey(p => p.ClienteId);
            
        modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Estabelecimento)
            .WithMany()
            .HasForeignKey(p => p.EstabelecimentoId);
            
        modelBuilder.Entity<ItemPedido>()
            .HasOne(i => i.Pedido)
            .WithMany(p => p.Itens)
            .HasForeignKey(i => i.PedidoId);
            
        modelBuilder.Entity<ItemPedido>()
            .HasOne(i => i.Produto)
            .WithMany()
            .HasForeignKey(i => i.ProdutoId);
            
        modelBuilder.Entity<ItemPedidoAdicional>()
            .HasOne(ia => ia.ItemPedido)
            .WithMany(i => i.Adicionais)
            .HasForeignKey(ia => ia.ItemPedidoId);
            
        modelBuilder.Entity<ItemPedidoAdicional>()
            .HasOne(ia => ia.Adicional)
            .WithMany()
            .HasForeignKey(ia => ia.AdicionalId);
    }
}
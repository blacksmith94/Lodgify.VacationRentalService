using Lodgify.VacationRentalService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lodgify.VacationRentalService.SqlDataAccess.Configs
{
	/// <summary>
	/// This class lets Entity Framework map the table design to the Rental model, all Rental table schema specifications should be defined here.
	/// </summary>
	public class RentalConfig : IEntityTypeConfiguration<Rental>
	{
		public void Configure(EntityTypeBuilder<Rental> builder)
		{
			builder.ToTable("rental");

			builder.Property(f => f.Id)
				.ValueGeneratedOnAdd();

			builder.Property(f => f.Units);
			
			builder.HasKey(f => f.Id);
		}
	}
}

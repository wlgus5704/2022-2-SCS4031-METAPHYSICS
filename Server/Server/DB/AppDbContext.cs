﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.DB
{
	public class AppDbContext : DbContext
	{
		public DbSet<AccountDb> Accounts { get; set; }

		// 로그 찍는데 사용
		static readonly ILoggerFactory _logger = LoggerFactory.Create(builder => { builder.AddConsole(); });

		string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GameDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			options
				.UseLoggerFactory(_logger)
				.UseSqlServer(_connectionString);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<AccountDb>()
				.HasIndex(a => a.AccountName)
				.IsUnique();
		}
	}
}

﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using vlko.BlogModule.NH.Tests.Repository.IOCResolved.Model;
using vlko.BlogModule.NH.Tests.Repository.IOCResolved.Queries;
using vlko.core.InversionOfControl;
using vlko.core.NH.Repository;
using vlko.core.NH.Repository.RepositoryAction;
using vlko.core.NH.Testing;
using vlko.core.Repository.RepositoryAction;

namespace vlko.BlogModule.NH.Tests.Repository.IOCResolved
{
	[TestClass]
	public class IoCNRepositoryLinqTest : InMemoryTest
	{
		private BaseTest _Test;

		[TestInitialize]
		public void Init()
		{
			IoC.AddCatalogAssembly(Assembly.Load("vlko.Core.NH"));
			IoC.AddCatalogAssembly(Assembly.Load("vlko.BlogModule"));
			IoC.AddCatalogAssembly(Assembly.Load("vlko.BlogModule.NH"));

			IoC.AddRerouting<ICreateCommand<Hotel>>(() => new CrudCommands<Hotel>());
			IoC.AddRerouting<ICreateCommand<Room>>(() => new CrudCommands<Room>());
			IoC.AddRerouting<ICreateCommand<Reservation>>(() => new CrudCommands<Reservation>());
			IoC.AddRerouting<IAllQuery<Hotel>>(() => new AllLinqQuery<Hotel>());
			IoC.AddRerouting<IHotelRoomsQuery>(() => new HotelRoomsLinqQuery());
			IoC.AddRerouting<IReservationForDayQuery>(() => new ReservationForDayLinqQuery());
			IoC.AddRerouting<IProjectionQuery>(() => new ProjectionLinqQuery());

			base.SetUp();
	  
			_Test = new BaseTest();
			_Test.Intialize();
		}

		[TestCleanup]
		public void Cleanup()
		{
			IoC.ClearReroutings();
			TearDown();
		}

		public override Type[] GetMappingTypes()
		{
			return new Type[] { typeof(Hotel), typeof(Room), typeof(Reservation) };
		}

		public override void ConfigureMapping(NHibernate.Cfg.Configuration configuration)
		{
            var mapper = new ConventionModelMapper();

            mapper.BeforeMapProperty += (inspector, member, customizer) =>
            {
                if (member.LocalMember.GetPropertyOrFieldType() == typeof(string))
                {
                    customizer.Length(50);
                }
            };
            mapper.BeforeMapClass += (inspector, type, customizer) => customizer.Id(im => im.Generator(Generators.GuidComb));
            // define the mapping shape

			mapper.Class<Hotel>(ca => ca.List(item => item.Reservations, cm => cm.Key(km => km.Column("HotelId"))));
            mapper.Class<Room>(ca => ca.List(item => item.Reservations, cm => cm.Key(km =>
			                                                                                   	{
			                                                                                   		km.Column("RoomId");
			                                                                                   		km.OnDelete(OnDeleteAction.NoAction);
			                                                                                   	})));
            mapper.Class<Reservation>(ca =>
			                              	{
												ca.ManyToOne(item => item.Hotel, m => { m.Column("HotelId"); m.Insert(false); m.Update(false); m.Lazy(LazyRelation.Proxy);} );
												ca.ManyToOne(item => item.Room, m => { m.Column("RoomId"); m.Insert(false); m.Update(false); m.Lazy(LazyRelation.Proxy); });
			                              	});

            // list all the entities we want to map.
            IEnumerable<Type> baseEntities = GetMappingTypes();

            // compile the mapping for the specified entities
			HbmMapping mappingDocument = mapper.CompileMappingFor(baseEntities);

			// inject the mapping in NHibernate
			configuration.AddDeserializedMapping(mappingDocument, "Domain");
			// fix up the schema
			SchemaMetadataUpdater.QuoteTableAndColumns(configuration);

			SessionFactory.SessionFactoryInstance = configuration.BuildSessionFactory();
		}

		[TestMethod]
		public virtual void QueryAllHotels()
		{
			_Test.QueryAllHotels();
		}

		[TestMethod]
		public virtual void QueryHotelRoomsByName()
		{
			_Test.QueryHotelRoomsByName();
		}

		[TestMethod]
		public virtual void QueryReservationsForDate()
		{
			_Test.QueryReservationsForDate();
		}


		[TestMethod]
		public virtual void QueryProjection()
		{
			_Test.QueryProjection();
		}
	}
}

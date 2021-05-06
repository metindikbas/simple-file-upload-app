using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using SimpleFileUpload.Application.Common.Interfaces;

namespace SimpleFileUpload.Application.UnitTests
{
    public static class TestHelpers
    {
        internal static DbSet<T> CreateMockDbSet<T>(List<T> entities) where T : class
        {
            var queryable = entities.AsQueryable();
            var dbSet = queryable.BuildMockDbSet();
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(entities.Add);
            dbSet.Setup(d => d.AddAsync(It.IsAny<T>(), It.IsAny<CancellationToken>()))
                .Callback<T, CancellationToken>((obj, token) => entities.Add(obj));
            dbSet.Setup(d => d.AddRange(It.IsAny<IEnumerable<T>>())).Callback<IEnumerable<T>>(entities.AddRange);
            dbSet.Setup(d => d.AddRangeAsync(It.IsAny<IEnumerable<T>>(), It.IsAny<CancellationToken>()))
                .Callback<IEnumerable<T>, CancellationToken>((obj, token) => entities.AddRange(obj));
            return dbSet.Object;
        }

        internal static IDateTimeProvider CreateDateTimeProvider(DateTime? now = null)
        {
            var mock = new Mock<IDateTimeProvider>();
            mock.Setup(x => x.Now).Returns(now ?? DateTime.Now);
            return mock.Object;
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Acme.BookStore.MultiTenancy;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Emailing;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.IdentityServer;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using System;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore
{
    [DependsOn(
        typeof(BookStoreDomainSharedModule),
        typeof(AbpAuditLoggingDomainModule),
        typeof(AbpBackgroundJobsDomainModule),
        typeof(AbpFeatureManagementDomainModule),
        typeof(AbpIdentityDomainModule),
        typeof(AbpPermissionManagementDomainIdentityModule),
        typeof(AbpIdentityServerDomainModule),
        typeof(AbpPermissionManagementDomainIdentityServerModule),
        typeof(AbpSettingManagementDomainModule),
        typeof(AbpTenantManagementDomainModule),
        typeof(AbpEmailingModule)
    )]
    public class BookStoreDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });

#if DEBUG
            context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif
        }
    }

    namespace Acme.BookStore
    {
        public class BookStoreDataSeederContributor
            : IDataSeedContributor, ITransientDependency
        {
            private readonly IRepository<Book, Guid> _bookRepository;

            public BookStoreDataSeederContributor(IRepository<Book, Guid> bookRepository)
            {
                _bookRepository = bookRepository;
            }

            public async Task SeedAsync(DataSeedContext context)
            {
                if (await _bookRepository.GetCountAsync() <= 0)
                {
                    await _bookRepository.InsertAsync(
                        new Book
                        {
                            Name = "1984",
                            Type = BookType.Dystopia,
                            PublishDate = new DateTime(1949, 6, 8),
                            Price = 19.84f
                        },
                        autoSave: true
                    );

                    await _bookRepository.InsertAsync(
                        new Book
                        {
                            Name = "The Hitchhiker's Guide to the Galaxy",
                            Type = BookType.ScienceFiction,
                            PublishDate = new DateTime(1995, 9, 27),
                            Price = 42.0f
                        },
                        autoSave: true
                    );
                }
            }
        }
    }
}

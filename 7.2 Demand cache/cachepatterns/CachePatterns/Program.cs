using CachePatterns;

IDataAccessor accessor = new TestDataAccessor();
accessor = new CacheAccessor(accessor);

var book = accessor.getBook(new Random().Next(1, 6));
var magazine = accessor.getMagazine(new Random().Next(1, 6));
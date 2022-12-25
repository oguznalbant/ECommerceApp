Basket microservice'inin discount yani indirimli ürün fiyatını göstermesi için discount.grpc kullanıyoruz.

ConnectedServiceden grpc seçip lokal konfigurasyon için file browsedan discount.grpc içerisindeki proto fileini seçiyoruz. 
Ve bunu client olarak consume edecek şekilde seçiyoruz. Doğru eklendiğini; csproj içerisinde itemgroupdan da check edebiliriz. Protos folderi da projeye eklendi.
proje build edildikten sonra vs obj->debug->.net6.0 içerisinde discount.grpc classını client modunda generate ettiğini göreceğiz.

shoppingcart a her yeni item eklendiğinde grpcye gidecek yani burada çok fazla çağrı olacak buna da inter-service communication diyoruz.

generate class ı direkt proje içerisinde kullanmak doğru değildir encapsulate yöntemiyle bi servis aracılığıyla consume edilmelidr.

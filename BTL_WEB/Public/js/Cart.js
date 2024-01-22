var CartClass = {
    Get: function () {
        var sIDSP = "";
        if (localStorage.getItem("arrIDSP") != null) {
            sIDSP = localStorage.getItem("arrIDSP");
        } else {
            localStorage.setItem("arrIDSP", "[]");
            sIDSP = localStorage.getItem("arrIDSP");
        }
        var arr = JSON.parse(sIDSP);
        return arr;
    },
    Set: function (arr) {
        var jsonIDSP = JSON.stringify(arr);
        localStorage.setItem("arrIDSP", jsonIDSP);
    },
    AddItem: function (id, quality) {
        //lay mang cu ra tu local
        var arr = CartClass.Get();
        //check exit
        var objIncIndex = arr.findIndex(obj => obj.ID === id);
        if (objIncIndex !== -1) {
            alert('Sản phẩm đã có trong giỏ hàng!')
            return;
        }
        //them vao
        var newID = {
            ID: id,
            /*Quality: quality*/
            Quality: quality
        }
        arr.push(newID);
        //set lai
        CartClass.Set(arr);
        //thông báo thành công
        alert('Đã thêm sản phẩm vào giỏ hàng!')
    },
    Remove: function (id) {
        var Cart = CartClass.Get();
        var newCart = [];
        $.each(Cart, function (index, item) {
            if (parseInt(item.ID) !== id) {
                newCart.push(item);
            }
        });
        CartClass.Set(newCart);
        $(".row_" + id).remove();
    },

    ChangeQuality: function (id, soLuong) {
        var Cart = CartClass.Get();
        $.each(Cart, function (index, item) {
            if (parseInt(item.ID) === id) {
                item.Quality = soLuong;
            }
        });
        CartClass.Set(Cart);
        CartClass.ViewXemGioHang();
    },
    IncreaseQuantity: function (id) {
        var Cart = CartClass.Get();
        $.each(Cart, function (index, item) {
            if (parseInt(item.ID) === id) {
                item.Quality++;
            }
        });
        CartClass.Set(Cart);
        CartClass.ViewXemGioHang();
    },

    DecreaseQuantity: function (id) {
        var Cart = CartClass.Get();
        $.each(Cart, function (index, item) {
            if (parseInt(item.ID) === id) {
                // Đảm bảo giảm xuống tối đa là 1
                item.Quality = Math.max(1, item.Quality - 1);
            }
        });
        CartClass.Set(Cart);
        CartClass.ViewXemGioHang();
    },
    ViewXemGioHang: function () {
        var cartItems = CartClass.Get();
        var newArray = [];
        $.each(cartItems, function (index, item) {
            var newID = {
                MaSP: item.ID,
                SOLUONG: item.Quality
            }
            newArray.push(newID);
        })
        var json = {
            sanPhams: newArray
        }
        $.ajax({
            url: "/Cart/LoadSanPham",
            type: "POST",
            data: json,
            dataType: 'html',
            success: function (data) {
                $("#cart-view").empty();
                $("#cart-view").append(data);
            }
        });
    }
}
Notiflix.Notify.init({
    width: '280px',
    position: 'right-top',
    distance: '10px',
    opacity: 1,
    borderRadius: '5px',
    rtl: false,
    timeout: 3000,
    messageMaxLength: 110,
    backOverlay: false,
    backOverlayColor: 'rgba(0,0,0,0.5)',
    plainText: true,
    showOnlyTheLastOne: true,
    clickToClose: false,
    pauseOnHover: true,
    ID: 'NotiflixNotify',
    className: 'notiflix-notify',
    zindex: 4001,
    fontFamily: 'Quicksand',
    fontSize: '13px',
    cssAnimation: true,
    cssAnimationDuration: 400,
    cssAnimationStyle: 'fade',
    closeButton: false,
    useIcon: true,
    useFontAwesome: false,
    fontAwesomeIconStyle: 'basic',
    fontAwesomeIconSize: '34px',
    success: {
        background: '#32c682',
        textColor: '#fff',
        childClassName: 'notiflix-notify-success',
        notiflixIconColor: 'rgba(0,0,0,0.2)',
        fontAwesomeClassName: 'fas fa-check-circle',
        fontAwesomeIconColor: 'rgba(0,0,0,0.2)',
        backOverlayColor: 'rgba(50,198,130,0.2)',
    },
    failure: {
        background: '#ff5549',
        textColor: '#fff',
        childClassName: 'notiflix-notify-failure',
        notiflixIconColor: 'rgba(0,0,0,0.2)',
        fontAwesomeClassName: 'fas fa-times-circle',
        fontAwesomeIconColor: 'rgba(0,0,0,0.2)',
        backOverlayColor: 'rgba(255,85,73,0.2)',
    },
    warning: {
        background: '#eebf31',
        textColor: '#fff',
        childClassName: 'notiflix-notify-warning',
        notiflixIconColor: 'rgba(0,0,0,0.2)',
        fontAwesomeClassName: 'fas fa-exclamation-circle',
        fontAwesomeIconColor: 'rgba(0,0,0,0.2)',
        backOverlayColor: 'rgba(238,191,49,0.2)',
    },
    info: {
        background: '#26c0d3',
        textColor: '#fff',
        childClassName: 'notiflix-notify-info',
        notiflixIconColor: 'rgba(0,0,0,0.2)',
        fontAwesomeClassName: 'fas fa-info-circle',
        fontAwesomeIconColor: 'rgba(0,0,0,0.2)',
        backOverlayColor: 'rgba(38,192,211,0.2)',
    },
});

function successMessage(message, url) {
    //Swal.fire({
    //    title: "Success!",
    //    text: message,
    //    icon: "success",
    //});

    Swal.fire({
        title: "Success!",
        text: message,
        icon: "success",
        showDenyButton: false,
        showCancelButton: false,
        confirmButtonText: "Ok",
    }).then((result) => {
        if (result.isConfirmed && url != undefined) {
            location.href = url;
        }
    });
}
function successFullyMessage(message) {

    Notiflix.Notify.success(message, {
        position: 'center-top',
        transitionOut: 'fadeOutUp'
    });
}
function confirmMessage(message) {
    return new Promise((resolve, reject) => {
        Swal.fire({
            title: "Confirm",
            text: message,
            icon: "warning",
            showCancelButton: true,
        }).then((result) => {
            // return result.isConfirmed;
            resolve(result.isConfirmed)
        });
    });
}


function warningMessage(message) {
    Swal.fire({
        title: "Warning!",
        text: message,
        icon: "warning",
    });
}

function errorMessage(message) {
    Swal.fire({
        title: "Error!",
        text: message,
        icon: "error",
    });
}

function showMessage(data, url) {
    if (data.isSuccess) {
        successMessage(data.message, url);
    }
    else {
        errorMessage(data.message);
    }
}
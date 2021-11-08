var resultModel = [];
var resultStat = [];
var resultPIU = [];
var resultMTS = [];
var resultDUE = [];
var selectWithdrawReport = "";

$(document).ready(function () {
    GetStatPO();
});

function GetStatPO() {
    $.ajax({
        url: urlGetStatPO,
        dataType: 'json',
        cache: false,
        success: function (data) {
            resultStat = data;
            console.log(JSON.stringify(resultStat))
            BindStat(resultStat);
            getMTS();
        }
    });
}
function getMTS() {
    $.ajax({
        url: urlGetStatMTS,
        dataType: 'json',
        cache: false,
        success: function (data) {
            resultMTS = data;
            BindMTS(resultMTS);
            getPIU();
        }
    });
}

function getPIU() {
    $.ajax({
        url: urlGetStatPIU,
        dataType: 'json',
        cache: false,
        success: function (data) {
            resultPIU = data;
            BindPIU(resultPIU);
            GetBooked();
        }
    });
}
function GetDue() {
    $.ajax({
        url: urlGetStatDUE,
        dataType: 'json',
        cache: false,
        success: function (data) {
            resultDUE = data;
            BindDUE(resultDUE);

        }
    });
}

function GetBooked() {
    $.ajax({
        url: urlGetBooked,
        dataType: 'json',
        cache: false,
        success: function (data) {
            resultBooked = data;
            BindBooked(resultBooked);
            GetDue();
        }
    });
}

function BindStat(result) {
    statusDS = [];
    var ListData = result;
    for (var i = 0; i < ListData.length; i++) {
        statusDS.push(ListData[i]);
    }
    for (i = 0; i < statusDS.length; i++) {
        statusDS[i].Number = (i + 1).toString();
    }

    bindgridStat();
}
function BindBooked(result) {
    BookedDS = [];
    var ListData = result;
    for (var i = 0; i < ListData.length; i++) {
        BookedDS.push(ListData[i]);
    }
    for (i = 0; i < BookedDS.length; i++) {
        BookedDS[i].Number = (i + 1).toString();
    }

    bindBooked();
}
function BindMTS(result) {

    productDS = [];
    var ListData = result;
    for (var i = 0; i < ListData.length; i++) {
        productDS.push(ListData[i]);
    }
    for (i = 0; i < productDS.length; i++) {
        productDS[i].Number = (i + 1).toString();
    }
    bindgridMTS();
}
function BindPIU(result) {
    bankDS = [];
    var ListData = result;
    for (var i = 0; i < ListData.length; i++) {
        bankDS.push(ListData[i]);
    }
    for (i = 0; i < bankDS.length; i++) {
        bankDS[i].Number = (i + 1).toString();
    }
    bindgridPIU();

}
function BindDUE(result) {

    freqDS = [];
    var ListData = result;
    for (var i = 0; i < ListData.length; i++) {
        freqDS.push(ListData[i]);
    }
    for (i = 0; i < freqDS.length; i++) {
        freqDS[i].Number = (i + 1).toString();
    }
    bindgridDue();

}

function onChange(arg) {
    selectWithdrawReport = this.selectedKeyNames().join(",");

}
function bindgridStat() {
    $("#GridStatusPO").kendoGrid({
        dataSource: {
            data: statusDS,
            schema: {
                model: {
                    id: "no",
                    fields: {
                        no: { type: "number" },
                        desc1: { type: "string" },
                        total: { type: "number" }


                    }
                }
            },
            pageSize: 5
        },
        filterable: true,
        groupable: false,
        sortable: true,
        noRecords: true,




		/* toolbar: ["excel"],
        excel: {
            fileName: "EDDA_Recieved.xlsx", allPages: true
        },  */
        pageable: {
            info: true,
            refresh: true,
            pageSizes: true,
            previousNext: true,
            numeric: true
        },
        columns: [

            { field: "no", title: "No", width: "30px", hidden: true, encoded: false },
            { field: "desc1", title: "Lokasi Simpan", width: "130px", encoded: false },
            { field: "total", title: "Total", width: "100px", encoded: false }



        ]
    });
}
function bindgridPIU() {
    $("#GridPiu").kendoGrid({
        dataSource: {
            data: bankDS,
            schema: {
                model: {
                    id: "no",
                    fields: {
                        no: { type: "number" },
                        desc1: { type: "string" },
                        total: { type: "number" }


                    }
                }
            },
            pageSize: 5
        },
        filterable: true,
        groupable: false,
        sortable: true,
        noRecords: true,




        /* 	toolbar: ["excel"],
            excel: {
                fileName: "EDDA_GridBank.xlsx", allPages: true
            },  */
        pageable: {
            info: true,
            refresh: true,
            pageSizes: true,
            previousNext: true,
            numeric: true
        },
        columns: [

            { field: "no", title: "No", width: "30px", hidden: true, encoded: false },
            { field: "desc1", title: "Desc1", width: "130px", encoded: false },
            { field: "total", title: "Total", width: "100px", encoded: false }



        ]
    });
}

function bindgridMTS() {
    $("#GridMTS").kendoGrid({
        dataSource: {
            data: productDS,
            schema: {
                model: {
                    id: "no",
                    fields: {
                        no: { type: "number" },
                        desc1: { type: "string" },
                        desc2: { type: "string" },
                        total: { type: "number" }


                    }
                }
            },
            pageSize: 5
        },
        filterable: true,
        groupable: false,
        sortable: true,
        noRecords: true,




        /* 	toolbar: ["excel"],
            excel: {
                fileName: "EDDA_GridBank.xlsx", allPages: true
            },  */
        pageable: {
            info: true,
            refresh: true,
            pageSizes: true,
            previousNext: true,
            numeric: true
        },
        columns: [

            { field: "no", title: "Number", width: "30px", hidden: true, encoded: false },
            { field: "desc1", title: "Gudang Asal", width: "130px", encoded: false },
            { field: "desc2", title: "Gudang Tujuan", width: "130px", encoded: false },
            { field: "total", title: "Total", width: "100px", encoded: false }



        ]
    });
}

function bindBooked() {
    $("#GridBooked").kendoGrid({
        dataSource: {
            data: BookedDS,
            schema: {
                model: {
                    id: "no",
                    fields: {
                        no: { type: "number" },
                        desc1: { type: "string" },
                        desc2: { type: "string" },
                        desc3: { type: "string" },
                        total: { type: "number" }


                    }
                }
            },
            pageSize: 5
        },
        filterable: true,
        groupable: false,
        sortable: true,
        noRecords: true,




        /* 	toolbar: ["excel"],
            excel: {
                fileName: "EDDA_GridBank.xlsx", allPages: true
            },  */
        pageable: {
            info: true,
            refresh: true,
            pageSizes: true,
            previousNext: true,
            numeric: true
        },
        columns: [

            { field: "no", title: "Number", width: "10px", hidden: true, encoded: false },
            { field: "desc1", title: "Cbg", width: "30px", filterable: false, encoded: false },
            { field: "desc2", title: "Periode", width: "40px", filterable: false, encoded: false },
            { field: "desc3", title: "Nama Barang", width: "180px", encoded: false },
            { field: "total", title: "Qty Booked", width: "70px", encoded: false }



        ]
    });
}
function bindgridDue() {
    $("#GridDue").kendoGrid({
        dataSource: {
            data: freqDS,
            schema: {
                model: {
                    id: "no",
                    fields: {
                        no: { type: "number" },
                        desc1: { type: "string" },
                        total: { type: "number" }


                    }
                }
            },
            pageSize: 5
        },
        filterable: true,
        groupable: false,
        sortable: true,
        //change: onChange,
        noRecords: true,




        /* 	toolbar: ["excel"],
            excel: {
                fileName: "EDDA_GridBank.xlsx", allPages: true
            },  */
        pageable: {
            info: true,
            refresh: true,
            pageSizes: true,
            previousNext: true,
            numeric: true
        },
        columns: [

            { field: "no", title: "Number", width: "30px", hidden: true, encoded: false },
            { field: "desc1", title: "Desc", width: "130px", encoded: false },
            { field: "total", title: "Total", width: "100px", encoded: false }



        ]
    });
}



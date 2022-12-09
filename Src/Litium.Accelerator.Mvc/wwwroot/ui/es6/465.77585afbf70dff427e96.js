(self.webpackChunk=self.webpackChunk||[]).push([[465,800],{3465:(e,t,a)=>{"use strict";a.r(t),a.d(t,{default:()=>O});var r=a(7378),l=a(8036),n=a(9453),m=a(5319),s=a(8997);const o=e=>({type:n.mj,payload:{mode:e}}),c=(e=1,t=!1,a=n.c_.PageSize,r=null,l=!1)=>o=>(0,m.U2)(`/api/order?pageIndex=${e}&showMyOrders=${t}&pageSize=${a}`).then((e=>e.json())).then((a=>{if(o(d(a.orders,a.totalCount,e,t,l?n.wO.Detail:n.wO.List)),r&&l){const e=a.orders.find((e=>e.orderId===r));o(u(e||{}))}})).catch((e=>o((0,s.K)(e,(e=>i(e)))))),d=(e,t,a,r,l=n.wO.List)=>({type:n.oM,payload:{list:e,mode:l,totalCount:t,currentPage:a,showOnlyMyOrders:r}}),i=e=>({type:n.OM,payload:{error:e}}),u=e=>({type:n.VT,payload:{order:e}});var E=a(4447),v=a(3800),g=a(5688);const p=({label:e,title:t,cssClass:a,orderId:l,onClick:n,callback:m})=>r.createElement("button",{onClick:()=>n({orderId:l,callback:m}),className:a,title:t||e},e),N=e=>{const t=(0,l.I0)(),a=(0,g.Z)(p,(({orderId:e,callback:a})=>{try{return t(((e,t)=>a=>(0,m.v_)("/api/order/approveOrder",{id:e}).then((e=>e.json())).then((e=>{t&&t(e)})).catch((e=>a((0,s.K)(e,(e=>i(e)))))))(e,a)),!0}catch(e){return!1}}),"buy-button");return r.createElement(a,e)},h=({onShowDetail:e,onApproveOrderCallback:t,isBusinessCustomer:a,hasApproverRole:n})=>{const m=(0,l.v9)((e=>e.myPage.orders.list));return r.createElement("div",{className:"order-history__list"},m?.length>0&&r.createElement("div",{className:"simple-table"},r.createElement("div",{className:"row medium-unstack no-margin simple-table__header hide-for-small-only"},r.createElement("div",{className:"columns medium-2"},(0,E.I)("orderlist.column.orderdate")),r.createElement("div",{className:"columns medium-6"},(0,E.I)("orderlist.column.content")),r.createElement("div",{className:"columns medium-2"},(0,E.I)("orderlist.column.grandtotal")),r.createElement("div",{className:"columns medium-2"},(0,E.I)("orderlist.column.status")),a&&r.createElement("div",{className:"columns medium-2 hide-for-small-only"})),m&&m.map((l=>r.createElement("div",{key:l.orderId,className:"row medium-unstack no-margin"},r.createElement("div",{className:"columns medium-2"},l.orderDate),r.createElement("div",{className:"columns medium-6"},r.createElement("a",{onClick:()=>e(l),className:"order-detail__product-link"},l.orderRows[0]?.brand,r.createElement("b",null,l.orderRows[0]?.name)," ",l.orderRows.length>1&&(0,E.I)(l.orderRows.length>2?"orderlist.items":"orderlist.item").replace("{0}",l.orderRows.length-1))),r.createElement("div",{className:"columns medium-2"},l.orderGrandTotal),r.createElement("div",{className:"columns medium-2"},l.status),a&&r.createElement("div",{className:"columns medium-2"},n&&l.isWaitingApprove&&r.createElement(N,{title:(0,E.I)("approve.label"),cssClass:"table__icon table__icon--accept",orderId:l.orderId,callback:t}),r.createElement(v.default,{title:(0,E.I)("general.reorder"),cssClass:"table__icon table__icon--reorder",orderId:l.orderId})))))),m?.length<=0&&r.createElement("div",null,(0,E.I)("orderlist.noorderfound")))},_=({onDismiss:e,onApproveOrderCallback:t,hasApproverRole:a})=>{const{order:n}=(0,l.v9)((e=>e.myPage.orders)),[m,s]=(0,r.useState)(!1);(0,r.useEffect)((()=>{s(a&&n?.isWaitingApprove)}),[a,n?.isWaitingApprove]);const o=(0,r.useCallback)((()=>{t&&t(n?.orderId,!0)}),[t,n?.orderId]);return r.createElement("div",{className:"row order-detail__container"},r.createElement("div",{className:"columns"},r.createElement("div",{className:"row-inner order-detail__button-container"},r.createElement("div",{className:"small-6"},r.createElement("a",{className:"order-detail__button",target:"_blank",rel:"noreferrer",href:`my-pages/order?id=${n?.orderId}&print=true`},(0,E.I)("general.print")),m&&r.createElement(N,{label:(0,E.I)("approve.label"),cssClass:"order-detail__button",orderId:n?.orderId,callback:o}),r.createElement(v.default,{label:(0,E.I)("general.reorder"),cssClass:"order-detail__button",orderId:n?.orderId})),r.createElement("div",{className:"small-6 text--right"},r.createElement("a",{className:"order-detail__button",onClick:e},(0,E.I)("orderdetail.backtoorderlist")))),r.createElement("div",{className:"row-inner"},r.createElement("div",{className:"medium-12 large-6"},r.createElement("h3",null,(0,E.I)("orderdetail.ordernumber"),":"," ",n?.externalOrderID)),r.createElement("div",{className:"medium-12 large-6 text--right text__mobile--left"},r.createElement("p",null,r.createElement("span",null,(0,E.I)("orderdetail.orderdate"),":"," ",n?.orderDate),r.createElement("br",null),r.createElement("span",null,(0,E.I)("orderdetail.orderstatus"),":"," ",r.createElement("strong",null,n?.status)),r.createElement("br",null),n?.formattedActualDeliveryDate&&r.createElement("span",null,(0,E.I)("orderdetail.deliverydate"),":"," ",r.createElement("strong",null,n?.formattedActualDeliveryDate))))),r.createElement("div",{className:"row-inner"},r.createElement("div",{className:"small-12"},r.createElement("div",{className:"row medium-unstack no-margin"},r.createElement("div",{className:"medium-12"},(0,E.I)("orderdetail.information"))),r.createElement("div",{className:"row no-margin"},r.createElement("div",{className:"medium-12"},r.createElement("p",null,n?.customerInfo?.address1,r.createElement("br",null),n?.customerInfo?.zip," ",n?.customerInfo?.city," ",r.createElement("br",null),n?.customerInfo?.country),r.createElement("p",null,(0,E.I)("orderdetail.organizationnumber"),": ",n?.merchantOrganizationNumber,r.createElement("br",null),(0,E.I)("orderdetail.orderreference"),": ",n?.customerInfo?.firstName," ",n?.customerInfo?.lastName))))),r.createElement("div",{className:"row-inner order-table"},r.createElement("div",{className:"row medium-unstack no-margin order-table__header hide-for-small-only"},r.createElement("div",{className:"columns medium-5"},(0,E.I)("orderdetail.column.products")),r.createElement("div",{className:"columns medium-1"},(0,E.I)("orderdetail.column.quantity")),r.createElement("div",{className:"columns medium-4"},(0,E.I)("orderdetail.column.price")),r.createElement("div",{className:"columns medium-2 text--right"},(0,E.I)("orderdetail.column.total"))),r.createElement("div",{className:"order-table__body"},n?.orderRows?.map(((e,t)=>r.createElement("div",{key:t,className:"row medium-unstack no-margin order-detail__summary-items"},r.createElement("div",{className:"columns medium-5"},r.createElement("a",{href:e.link?.href,target:"_parent",className:"order-detail__product-link"},e.brand," ",r.createElement("strong",null,e.name))),r.createElement("div",{className:"columns medium-1"},e.quantityString),r.createElement("div",{className:"columns medium-4"},e.isFreeGift&&e.priceInfo?.formattedCampaignPrice?r.createElement("span",null,e.priceInfo?.formattedCampaignPrice):r.createElement("span",null,e.priceInfo?.formattedPrice)),r.createElement("div",{className:"columns medium-2 text--right"},e.totalPrice)))),n?.discountRows?.map(((e,t)=>r.createElement("div",{key:t,className:"row medium-unstack no-margin order-detail__summary-items"},r.createElement("div",{className:"columns medium-5"},e.name),r.createElement("div",{className:"columns medium-7 text--right"},e.totalPrice)))),r.createElement("div",{className:"row medium-unstack no-margin order-detail__summary-method"},r.createElement("div",{className:"columns medium-9"},(0,E.I)("orderdetail.paymentmethod")," -",n?.paymentMethod),r.createElement("div",{className:"columns medium-3 text--right"},n?.orderTotalFee)),r.createElement("div",{className:"row medium-unstack no-margin order-detail__summary-method"},r.createElement("div",{className:"columns medium-9"},(0,E.I)("orderdetail.deliverymethod")," -"," ",n?.deliveryMethod),r.createElement("div",{className:"columns medium-3 text--right"},n?.orderTotalDeliveryCost)),n?.orderTotalDiscountAmount&&r.createElement("div",{className:"row medium-unstack no-margin order-detail__summary-method"},r.createElement("div",{className:"columns medium-9"},(0,E.I)("orderdetail.discount")),r.createElement("div",{className:"columns medium-3 text--right"},n?.orderTotalDiscountAmount)),r.createElement("div",{className:"row medium-unstack no-margin order-table__space-delimiter"}),r.createElement("div",{className:"row medium-unstack no-margin"},r.createElement("div",{className:"columns small-12 text--right"},(0,E.I)("orderdetail.grandtotal"),":"," ",r.createElement("strong",null,n?.orderGrandTotal))),r.createElement("div",{className:"row medium-unstack no-margin"},r.createElement("div",{className:"columns small-12 text--right"},(0,E.I)("orderdetail.ordertotalvat"),":"," ",n?.orderTotalVat))))))},y=({onDismiss:e})=>{const{order:t}=(0,l.v9)((e=>e.myPage.orders));return r.createElement("div",{className:"row order-detail__container"},r.createElement("div",{className:"columns"},r.createElement("div",{className:"row-inner order-detail__button-container"},r.createElement("div",{className:"small-4"},r.createElement("a",{className:"order-detail__button",target:"_blank",rel:"noreferrer",href:`my-pages/order?id=${t?.orderId}&print=true`},(0,E.I)("general.print"))),r.createElement("div",{className:"small-8 text--right"},r.createElement("a",{className:"order-detail__button",onClick:e},(0,E.I)("orderdetail.backtoorderlist")))),r.createElement("div",{className:"row-inner"},r.createElement("div",{className:"medium-12 large-6"},r.createElement("h3",null,(0,E.I)("orderdetail.ordernumber"),":"," ",t?.externalOrderID)),r.createElement("div",{className:"medium-12 large-6 text--right text__mobile--left"},r.createElement("p",null,(0,E.I)("orderdetail.orderdate"),":"," ",t?.orderDate," ",r.createElement("br",null),(0,E.I)("orderdetail.orderstatus"),":"," ",r.createElement("strong",null," ",t?.status)))),t?.formattedActualDeliveryDate&&r.createElement("div",{className:"row-inner"},r.createElement("div",{className:"medium-12 large-6 large-offset-6 text--right text__mobile--left"},(0,E.I)("orderdetail.deliverydate"),":"," ",r.createElement("strong",null,t?.formattedActualDeliveryDate))),r.createElement("div",{className:"row-inner order-table"},r.createElement("div",{className:"small-12"},r.createElement("div",{className:"row medium-unstack no-margin order-table__header"},r.createElement("div",{className:"medium-12 columns"},(0,E.I)("orderdetail.information"))),t?.deliveries?.map(((e,t)=>r.createElement("div",{key:t,className:"row no-margin order-table__body"},r.createElement("div",{className:"medium-12 columns"},r.createElement("p",null,e.address.firstName," ",e.address.lastName," ",r.createElement("br",null),e.address.address1," ",r.createElement("br",null),e.address.zip," ",e.address.city," ",r.createElement("br",null),e.address.country))))))),r.createElement("div",{className:"row-inner order-table"},r.createElement("div",{className:"row medium-unstack no-margin order-table__header hide-for-small-only"},r.createElement("div",{className:"columns medium-5"},(0,E.I)("orderdetail.column.products")),r.createElement("div",{className:"columns medium-2"},(0,E.I)("orderdetail.column.quantity")),r.createElement("div",{className:"columns medium-2"},(0,E.I)("orderdetail.column.price")),r.createElement("div",{className:"columns medium-3 text--right"},(0,E.I)("orderdetail.column.total"))),r.createElement("div",{className:"order-table__body"},t?.orderRows?.map(((e,t)=>r.createElement("div",{key:t,className:"row medium-unstack no-margin order-detail__summary-items"},r.createElement("div",{className:"columns medium-5"},r.createElement("a",{href:e.link?.href,target:"_parent",className:"order-detail__product-link"},e.brand," ",r.createElement("strong",null,e.name))),r.createElement("div",{className:"columns medium-2"},e.quantityString),r.createElement("div",{className:"columns medium-2"},e.isFreeGift&&e.priceInfo?.formattedCampaignPrice?r.createElement("span",null,e.priceInfo?.formattedCampaignPrice):r.createElement("span",null,e.priceInfo?.formattedPrice)),r.createElement("div",{className:"columns medium-3 text--right"},e.totalPrice)))),t?.discountRows?.map(((e,t)=>r.createElement("div",{key:t,className:"row medium-unstack no-margin order-detail__summary-items"},r.createElement("div",{className:"columns medium-5"},e.name),r.createElement("div",{className:"columns medium-7 text--right"},e.totalPrice)))),r.createElement("div",{className:"row medium-unstack no-margin order-detail__summary-method"},r.createElement("div",{className:"columns medium-9"},(0,E.I)("orderdetail.paymentmethod")," -"," ",t?.paymentMethod),r.createElement("div",{className:"columns medium-3 text--right"},t?.orderTotalFee)),r.createElement("div",{className:"row medium-unstack no-margin order-detail__summary-method"},r.createElement("div",{className:"columns medium-9"},(0,E.I)("orderdetail.deliverymethod")," -"," ",t?.deliveryMethod),r.createElement("div",{className:"columns medium-3 text--right"},t?.orderTotalDeliveryCost)),t?.orderTotalDiscountAmount&&r.createElement("div",{className:"row medium-unstack no-margin order-detail__summary-method"},r.createElement("div",{className:"columns medium-9"},(0,E.I)("orderdetail.discount")),r.createElement("div",{className:"columns medium-3 text--right"},t?.orderTotalDiscountAmount)),r.createElement("div",{className:"row medium-unstack no-margin order-table__space-delimiter"}),r.createElement("div",{className:"row medium-unstack no-margin"},r.createElement("div",{className:"columns small-12 text--right"},(0,E.I)("orderdetail.grandtotal"),":"," ",r.createElement("strong",null,t?.orderGrandTotal))),r.createElement("div",{className:"row medium-unstack no-margin"},r.createElement("div",{className:"columns small-12 text--right"},(0,E.I)("orderdetail.ordertotalvat"),":"," ",t?.orderTotalVat))))))},b=({name:e="",current:t=!1,disabled:a=!1,onChange:l})=>{const n=`pagination__link \n    ${t?"pagination__link--current":""} \n    ${a?"pagination__link--disabled":""}`;return r.createElement("li",{className:"pagination__item"},r.createElement("a",{className:n.trim(),onClick:()=>l()},e))},I=({intervalStart:e,edgeEntries:t,currentPageIndex:a,onChange:l})=>{const n=[],m=Math.min(t,e);for(let e=0;e<m;e++){const t=e+1;n.push(r.createElement(b,{key:t,name:t,current:t===a,onChange:()=>l(t)}))}return t<e&&n.push(r.createElement(b,{key:"first_indicator",name:"...",disabled:!0})),n},k=({intervalStart:e,intervalEnd:t,currentPageIndex:a,onChange:l})=>{const n=[];for(let m=e;m<t;m++){const e=m+1;n.push(r.createElement(b,{key:e,name:e,current:e===a,onChange:()=>l(e)}))}return n},f=({intervalEnd:e,edgeEntries:t,pageCount:a,currentPageIndex:l,onChange:n})=>{const m=[];a-t>e&&m.push(r.createElement(b,{key:"second_indicator",name:"...",disabled:!0}));for(let s=Math.max(a-t,e);s<a;s++){const e=s+1;m.push(r.createElement(b,{key:e,name:e,current:e===l,onChange:()=>n(e)}))}return m},w=({model:e,onChange:t})=>{const{currentPageIndex:a,pageCount:l,intervalStart:n,intervalEnd:m,edgeEntries:s}=e;return r.createElement(r.Fragment,null,l>1&&r.createElement("ul",{className:"pagination"},a>1&&r.createElement(b,{name:"<<",current:!1,disabled:!1,onChange:()=>t(a-1)}),n>0&&s>0&&r.createElement(I,{intervalStart:n,edgeEntries:s,currentPageIndex:a,onChange:t}),r.createElement(k,{intervalStart:n,intervalEnd:m,currentPageIndex:a,onChange:t}),m<l&&s>0&&r.createElement(f,{intervalEnd:m,pageCount:l,edgeEntries:s,currentPageIndex:a,onChange:t}),a<l&&r.createElement(b,{name:">>",current:!1,disabled:!1,onChange:()=>t(a+1)})))},C=(e,t,a)=>{const r=t-1,l=parseInt(Math.ceil(parseFloat(a/2))),n=e-a;return[r>l?Math.max(Math.min(r-l,n),0):0,r>l?Math.min(r+l,e):Math.min(a,e)]};var x=a(8844);const O=()=>{const{mode:e,totalCount:t,currentPage:a,showOnlyMyOrders:m,errors:s}=(0,l.v9)((e=>e.myPage.orders)),{isBusinessCustomer:d,hasApproverRole:i}=(0,l.v9)((e=>e.myPage)),[v,g]=(0,r.useState)(m||!1),[p,N]=(0,r.useState)({}),b=(0,l.I0)();(0,r.useEffect)((()=>{b(c(a,v))}),[b,a,v]),(0,r.useEffect)((()=>{N(((e=0,t=1,a={})=>{const{pageSize:r=n.c_.PageSize,displayedEntries:l=n.c_.DisplayedEntries,edgeEntries:m=n.c_.EdgeEntries}=a,s=0!=r?parseInt(Math.ceil(parseFloat(e)/r)):0,o=C(s,t,l);return{totalCount:e,pageSize:r,currentPageIndex:t,pageCount:s,edgeEntries:m,intervalStart:o[0],intervalEnd:o[1]}})(t,a))}),[t,a]);const I=(0,r.useCallback)((e=>{b(u(e)),b(o(n.wO.Detail))}),[b]),k=(0,r.useCallback)((()=>{b(u({})),b(o(n.wO.List))}),[b]),f=(0,r.useCallback)((e=>{g(e)}),[]),O=(0,r.useCallback)((e=>{e!==a&&b((e=>({type:n.EJ,payload:{currentPage:e}}))(e))}),[a,b]),D=(0,r.useCallback)(((e,t=!1)=>{b(c(a,v,n.c_.PageSize,e,t))}),[a,v,b]);return r.createElement(r.Fragment,null,s&&!(0,x.Z)(s)&&r.createElement(r.Fragment,null,Object.keys(s).map(((e,t)=>r.createElement("div",{key:t,className:"form__validator--error"},(0,E.I)(s[e]))))),e!==n.wO.List&&r.createElement(r.Fragment,null,d?r.createElement(_,{hasApproverRole:i,onApproveOrderCallback:D,onDismiss:k}):r.createElement(y,{onDismiss:k})),e===n.wO.List&&r.createElement(r.Fragment,null,d&&i&&r.createElement("div",{className:"order__checkbox-container"},r.createElement("a",{className:"order__checkbox-input"},r.createElement("input",{className:"form__radio",id:"showOnlyMyOrders",name:"showOnlyMyOrders",type:"checkbox",defaultChecked:v,onChange:e=>f(e.target.checked)}),r.createElement("label",{htmlFor:"showOnlyMyOrders"},(0,E.I)("orderlist.showonlymyorders")))),r.createElement(h,{onShowDetail:I,onApproveOrderCallback:D,isBusinessCustomer:d,hasApproverRole:i}),r.createElement(w,{model:p,onChange:O})))}},3800:(e,t,a)=>{"use strict";a.r(t),a.d(t,{default:()=>d});var r=a(7378),l=a(8036),n=a(2428),m=a(5688),s=a(4336),o=a(8997);const c=({label:e,title:t,cssClass:a,orderId:l,onClick:n})=>r.createElement("button",{className:a,type:"button",title:t,onClick:()=>n({orderId:l})},e),d=e=>{const t=(0,l.I0)(),a=(0,m.Z)(c,(async({orderId:e})=>{try{const a=await(0,n.H)(e);return t((0,s.pT)(a)),!0}catch(e){return t((0,o.K)(e,(e=>(0,s.Lx)(e)))),!1}}),"buy-button");return r.createElement(a,e)}},5688:(e,t,a)=>{"use strict";a.d(t,{Z:()=>o});var r=a(7378);function l(){return(l=Object.assign||function(e){for(var t=1;t<arguments.length;t++){var a=arguments[t];for(var r in a)Object.prototype.hasOwnProperty.call(a,r)&&(e[r]=a[r])}return e}).apply(this,arguments)}const n="--loading",m="--success",s="--error";function o(e,t,a){return o=>{const[c,d]=(0,r.useState)(""),i=(0,r.useRef)(0);(0,r.useEffect)((()=>()=>{d("")}),[]);const u=e=>{d(`${a}${e?m:s}`),!1!==o.autoReset&&setTimeout((()=>{d(""),i.current=0}),o.resetTimeout||2e3)};async function E(e){d(`${a}${n}`),i.current=Date.now();!function(e){const t=Date.now()-i.current,a=o.minimumLoadingTime||1e3;t>=a?u(e):setTimeout((()=>{u(e)}),a-t)}(await t(e))}return r.createElement("span",{className:c},r.createElement(e,l({onClick:e=>E(e)},o)))}}},2428:(e,t,a)=>{"use strict";a.d(t,{I:()=>l,H:()=>n});var r=a(5319);const l=async({articleNumber:e,quantity:t=1})=>{if(!t||isNaN(t)||parseFloat(t)<=0)throw"Invalid quantity";return(await(0,r.v_)("/api/cart/add",{articleNumber:e,quantity:parseFloat(t)})).json()},n=async e=>(await(0,r.v_)("/api/cart/reorder",{orderId:e})).json()}}]);
//# sourceMappingURL=465.77585afbf70dff427e96.js.map
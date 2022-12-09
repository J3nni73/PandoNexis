(self.webpackChunk=self.webpackChunk||[]).push([[41],{5041:(e,a,t)=>{"use strict";t.r(a),t.d(a,{default:()=>N});var l=t(7378),s=t(8036),r=t(4447),m=t(9453),n=t(5319),c=t(8997);const o="/api/mypageaddress",d=e=>({type:m.W6,payload:{mode:e}}),i=(e=m.wO.List)=>a=>(0,n.U2)(o).then((e=>e.json())).then((t=>{a(u(t,e))})).catch((e=>a((0,c.K)(e,(e=>p(e)))))),u=(e,a)=>({type:m.h8,payload:{list:e,mode:a}}),p=e=>({type:m.AT,payload:{error:e}}),_=({onEdit:e})=>{const a=(0,s.I0)(),t=(0,s.v9)((e=>e.myPage.addresses.list)),[m,d]=(0,l.useState)({}),u=(e,a)=>{d((t=>({...t,[e]:a})))};return l.createElement("div",{className:"simple-table"},l.createElement("div",{className:"row medium-unstack no-margin simple-table__header"},l.createElement("div",{className:"columns"},(0,r.I)("mypage.address.address")),l.createElement("div",{className:"columns"},(0,r.I)("mypage.address.postnumber")),l.createElement("div",{className:"columns"},(0,r.I)("mypage.address.city")),l.createElement("div",{className:"columns medium-2 hide-for-small-only"})),t&&t.map((t=>l.createElement("div",{className:"row medium-unstack no-margin",key:`${t.systemId}`},l.createElement("div",{className:"columns"},t.address),l.createElement("div",{className:"columns"},t.zipCode),l.createElement("div",{className:"columns"},t.city),l.createElement("div",{className:"columns medium-2"},l.createElement("a",{className:"table__icon table__icon--edit",onClick:()=>e(t),title:(0,r.I)("Edit")}),!m[t.systemId]&&l.createElement("a",{className:"table__icon table__icon--delete",onClick:()=>u(t.systemId,!0),title:(0,r.I)("Remove")}),m[t.systemId]&&l.createElement("a",{className:"table__icon table__icon--accept",onClick:()=>{return a((e=t.systemId,a=>(0,n.Od)(o,e).then((()=>a(i()))).catch((e=>a((0,c.K)(e,(e=>p(e))))))));var e},title:(0,r.I)("Accept")}),m[t.systemId]&&l.createElement("a",{className:"table__icon table__icon--cancel",onClick:()=>u(t.systemId,!1),title:(0,r.I)("Cancel")}))))))};var v=t(2479);const y=(0,v.Ry)().shape({phoneNumber:(0,v.Z_)().required((0,r.I)("validation.required")),country:(0,v.Z_)().required((0,r.I)("validation.required")),city:(0,v.Z_)().required((0,r.I)("validation.required")),zipCode:(0,v.Z_)().required((0,r.I)("validation.required")),address2:(0,v.Z_)().nullable(),address:(0,v.Z_)().required((0,r.I)("validation.required"))}),E=({address:e,onDismiss:a})=>{const t=(0,s.I0)(),d=(0,s.v9)((e=>e.myPage.addresses.errors))||{},[u,_]=(0,l.useState)(e);(0,l.useEffect)((()=>{_(e)}),[_,e]);const v=(e,a)=>{_((t=>({...t,[e]:a})))};return l.createElement("div",null,l.createElement("h2",null,(0,r.I)(e.systemId?"mypage.address.edittitle":"mypage.address.addtitle")),l.createElement("div",{className:"row"},l.createElement("div",{className:"columns small-12 medium-8"},l.createElement("label",{className:"form__label",htmlFor:"address"},(0,r.I)("mypage.address.address")),l.createElement("input",{className:"form__input",id:"address",name:"address",type:"text",autoComplete:"address-line1",value:u.address||"",onChange:e=>v("address",e.target.value)}),d.address&&l.createElement("span",{className:"form__validator--error form__validator--top-narrow"},d.address[0]),l.createElement("input",{className:"form__input",id:"address2",name:"address2",type:"text",autoComplete:"address-line2",value:u.address2||"",onChange:e=>v("address2",e.target.value)}),d.address2&&l.createElement("span",{className:"form__validator--error form__validator--top-narrow"},d.address2[0])),l.createElement("div",{className:"columns small-12 medium-8"},l.createElement("label",{className:"form__label",htmlFor:"zipCode"},(0,r.I)("mypage.address.postnumber")),l.createElement("input",{className:"form__input",id:"zipCode",name:"zipCode",type:"text",autoComplete:"postal-code",value:u.zipCode||"",onChange:e=>v("zipCode",e.target.value)}),d.zipCode&&l.createElement("span",{className:"form__validator--error form__validator--top-narrow"},d.zipCode[0])),l.createElement("div",{className:"columns small-12 medium-8"},l.createElement("label",{className:"form__label",htmlFor:"city"},(0,r.I)("mypage.address.city")),l.createElement("input",{className:"form__input",id:"city",name:"city",type:"text",autoComplete:"on",value:u.city||"",onChange:e=>v("city",e.target.value)}),d.city&&l.createElement("span",{className:"form__validator--error form__validator--top-narrow"},d.city[0])),l.createElement("div",{className:"columns small-12 medium-8"},l.createElement("label",{className:"form__label",htmlFor:"country"},(0,r.I)("mypage.address.country")),l.createElement("select",{className:"form__input",autoComplete:"country-name",value:u.country||"",onChange:e=>v("country",e.target.value)},m.ZP.countries.map((e=>l.createElement("option",{key:e.value,value:e.value},e.text)))),d.country&&l.createElement("span",{className:"form__validator--error form__validator--top-narrow"},d.country[0])),l.createElement("div",{className:"columns small-12 medium-8"},l.createElement("label",{className:"form__label",htmlFor:"phoneNumber"},(0,r.I)("mypage.address.phonenumber")),l.createElement("input",{className:"form__input",id:"phoneNumber",name:"phoneNumber",type:"tel",autoComplete:"tel",value:u.phoneNumber||"",onChange:e=>v("phoneNumber",e.target.value)}),d.phoneNumber&&l.createElement("span",{className:"form__validator--error form__validator--top-narrow"},d.phoneNumber[0]))),d.general&&l.createElement("div",null,d.general[0]),l.createElement("button",{className:"form__button",onClick:a},(0,r.I)("general.cancel")),l.createElement("span",{className:"form__space"}),l.createElement("button",{className:"form__button",onClick:()=>{y.validate(u).then((()=>{u.systemId?t((e=>a=>(0,n.gz)(o,e).then((()=>a(i()))).catch((e=>a((0,c.K)(e,(e=>p(e)))))))(u)):t((e=>a=>(0,n.v_)(o,e).then((()=>a(i()))).catch((e=>a((0,c.K)(e,(e=>p(e)))))))(u))})).catch((e=>t(p(e))))}},(0,r.I)("general.save")))},N=()=>{const e=(0,s.v9)((e=>e.myPage.addresses.mode)),a=(0,s.I0)();(0,l.useEffect)((()=>{a(i())}),[a]);const[t,n]=(0,l.useState)({}),c=(0,l.useCallback)((e=>{n(e),a(d(m.wO.Edit))}),[n,a]),o=(0,l.useCallback)((()=>{n({}),a(d(m.wO.List))}),[n,a]);return l.createElement(l.Fragment,null,e!==m.wO.List&&l.createElement(E,{address:t,onDismiss:o}),e===m.wO.List&&l.createElement(l.Fragment,null,l.createElement("h2",null,(0,r.I)("mypage.address.title")),l.createElement("p",null,l.createElement("b",null,(0,r.I)("mypage.address.subtitle"))),l.createElement("button",{className:"form__button",onClick:()=>c({country:m.ZP.countries[0].value})},(0,r.I)("mypage.address.add")),l.createElement(_,{onEdit:c})))}}}]);
//# sourceMappingURL=41.4577e1c2fef52d2a22d8.js.map
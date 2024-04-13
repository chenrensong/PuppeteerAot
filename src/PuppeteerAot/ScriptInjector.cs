using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuppeteerAot
{
    public class ScriptInjector
    {
        private static string _injectedSource;
        private readonly List<string> _amendments = new();
        private bool _updated = false;

        public void Append(string statement) => Update(() => _amendments.Add(statement));

        public void Pop(string statement) => Update(() => _amendments.Remove(statement));

        public string Get()
        {
            var amendments = string.Join(string.Empty, _amendments.Select(statement => $"({statement})(module.exports.default);"));

            return $@"(() => {{
                const module = {{}};
                {GetInjectedSource()}
                {amendments}
                return module.exports.default;
            }})()";
        }

        public async Task InjectAsync(Func<string, Task> inject, bool force = false)
        {
            if (_updated || force)
            {
                await inject(Get()).ConfigureAwait(false);
            }

            _updated = false;
        }

        private static string GetInjectedSource()
        {
            if (string.IsNullOrEmpty(_injectedSource))
            {
                _injectedSource = "\"use strict\";var __defProp=Object.defineProperty,__getOwnPropDesc=Object.getOwnPropertyDescriptor,__getOwnPropNames=Object.getOwnPropertyNames,__hasOwnProp=Object.prototype.hasOwnProperty,__export=(e,t)=>{for(var r in t)__defProp(e,r,{get:t[r],enumerable:!0})},__copyProps=(e,t,r,o)=>{if(t&&\"object\"==typeof t||\"function\"==typeof t)for(let s of __getOwnPropNames(t))__hasOwnProp.call(e,s)||s===r||__defProp(e,s,{get:()=>t[s],enumerable:!(o=__getOwnPropDesc(t,s))||o.enumerable});return e},__toCommonJS=e=>__copyProps(__defProp({},\"__esModule\",{value:!0}),e),injected_exports={};__export(injected_exports,{default:()=>injected_default}),module.exports=__toCommonJS(injected_exports);var CustomError=class extends Error{constructor(e){super(e),this.name=this.constructor.name,Error.captureStackTrace(this,this.constructor)}},TimeoutError=class extends CustomError{},ProtocolError=class extends CustomError{#e;#t=\"\";set code(e){this.#e=e}get code(){return this.#e}set originalMessage(e){this.#t=e}get originalMessage(){return this.#t}},errors=Object.freeze({TimeoutError:TimeoutError,ProtocolError:ProtocolError});function createDeferredPromise(e){let t,r,o=!1,s=!1;const n=new Promise(((e,o)=>{t=e,r=o})),i=e&&e.timeout>0?setTimeout((()=>{s=!0,r(new TimeoutError(e.message))}),e.timeout):void 0;return Object.assign(n,{resolved:()=>o,finished:()=>o||s,resolve:e=>{i&&clearTimeout(i),o=!0,t(e)},reject:e=>{clearTimeout(i),s=!0,r(e)}})}var createdFunctions=new Map,createFunction=e=>{let t=createdFunctions.get(e);return t||(t=new Function(`return ${e}`)(),createdFunctions.set(e,t),t)},ARIAQuerySelector_exports={};__export(ARIAQuerySelector_exports,{ariaQuerySelector:()=>ariaQuerySelector});var ariaQuerySelector=(e,t)=>window.__ariaQuerySelector(e,t),CustomQuerySelector_exports={};__export(CustomQuerySelector_exports,{customQuerySelectors:()=>customQuerySelectors});var CustomQuerySelectorRegistry=class{#r=new Map;register(e,t){if(!t.queryOne&&t.queryAll){const e=t.queryAll;t.queryOne=(t,r)=>{for(const o of e(t,r))return o;return null}}else if(t.queryOne&&!t.queryAll){const e=t.queryOne;t.queryAll=(t,r)=>{const o=e(t,r);return o?[o]:[]}}else if(!t.queryOne||!t.queryAll)throw new Error(\"At least one query method must be defined.\");this.#r.set(e,{querySelector:t.queryOne,querySelectorAll:t.queryAll})}unregister(e){this.#r.delete(e)}get(e){return this.#r.get(e)}clear(){this.#r.clear()}},customQuerySelectors=new CustomQuerySelectorRegistry,PierceQuerySelector_exports={};__export(PierceQuerySelector_exports,{pierceQuerySelector:()=>pierceQuerySelector,pierceQuerySelectorAll:()=>pierceQuerySelectorAll});var pierceQuerySelector=(e,t)=>{let r=null;const o=e=>{const s=document.createTreeWalker(e,NodeFilter.SHOW_ELEMENT);do{const n=s.currentNode;n.shadowRoot&&o(n.shadowRoot),n instanceof ShadowRoot||n!==e&&!r&&n.matches(t)&&(r=n)}while(!r&&s.nextNode())};return e instanceof Document&&(e=e.documentElement),o(e),r},pierceQuerySelectorAll=(e,t)=>{const r=[],o=e=>{const s=document.createTreeWalker(e,NodeFilter.SHOW_ELEMENT);do{const n=s.currentNode;n.shadowRoot&&o(n.shadowRoot),n instanceof ShadowRoot||n!==e&&n.matches(t)&&r.push(n)}while(s.nextNode())};return e instanceof Document&&(e=e.documentElement),o(e),r},assert=(e,t)=>{if(!e)throw new Error(t)},MutationPoller=class{#o;#s;#n;#i;constructor(e,t){this.#o=e,this.#s=t}async start(){const e=this.#i=createDeferredPromise(),t=await this.#o();t?e.resolve(t):(this.#n=new MutationObserver((async()=>{const t=await this.#o();t&&(e.resolve(t),await this.stop())})),this.#n.observe(this.#s,{childList:!0,subtree:!0,attributes:!0}))}async stop(){assert(this.#i,\"Polling never started.\"),this.#i.finished()||this.#i.reject(new Error(\"Polling stopped\")),this.#n&&(this.#n.disconnect(),this.#n=void 0)}result(){return assert(this.#i,\"Polling never started.\"),this.#i}},RAFPoller=class{#o;#i;constructor(e){this.#o=e}async start(){const e=this.#i=createDeferredPromise(),t=await this.#o();if(t)return void e.resolve(t);const r=async()=>{if(e.finished())return;const t=await this.#o();t?(e.resolve(t),await this.stop()):window.requestAnimationFrame(r)};window.requestAnimationFrame(r)}async stop(){assert(this.#i,\"Polling never started.\"),this.#i.finished()||this.#i.reject(new Error(\"Polling stopped\"))}result(){return assert(this.#i,\"Polling never started.\"),this.#i}},IntervalPoller=class{#o;#l;#a;#i;constructor(e,t){this.#o=e,this.#l=t}async start(){const e=this.#i=createDeferredPromise(),t=await this.#o();t?e.resolve(t):this.#a=setInterval((async()=>{const t=await this.#o();t&&(e.resolve(t),await this.stop())}),this.#l)}async stop(){assert(this.#i,\"Polling never started.\"),this.#i.finished()||this.#i.reject(new Error(\"Polling stopped\")),this.#a&&(clearInterval(this.#a),this.#a=void 0)}result(){return assert(this.#i,\"Polling never started.\"),this.#i}},TRIVIAL_VALUE_INPUT_TYPES=new Set([\"checkbox\",\"image\",\"radio\"]),isNonTrivialValueNode=e=>e instanceof HTMLSelectElement||(e instanceof HTMLTextAreaElement||e instanceof HTMLInputElement&&!TRIVIAL_VALUE_INPUT_TYPES.has(e.type)),UNSUITABLE_NODE_NAMES=new Set([\"SCRIPT\",\"STYLE\"]),isSuitableNodeForTextMatching=e=>!UNSUITABLE_NODE_NAMES.has(e.nodeName)&&!document.head?.contains(e),textContentCache=new WeakMap,eraseFromCache=e=>{for(;e;)textContentCache.delete(e),e=e instanceof ShadowRoot?e.host:e.parentNode},observedNodes=new WeakSet,textChangeObserver=new MutationObserver((e=>{for(const t of e)eraseFromCache(t.target)})),createTextContent=e=>{let t=textContentCache.get(e);if(t)return t;if(t={full:\"\",immediate:[]},!isSuitableNodeForTextMatching(e))return t;let r=\"\";if(isNonTrivialValueNode(e))t.full=e.value,t.immediate.push(e.value),e.addEventListener(\"input\",(e=>{eraseFromCache(e.target)}),{once:!0,capture:!0});else{for(let o=e.firstChild;o;o=o.nextSibling)o.nodeType!==Node.TEXT_NODE?(r&&t.immediate.push(r),r=\"\",o.nodeType===Node.ELEMENT_NODE&&(t.full+=createTextContent(o).full)):(t.full+=o.nodeValue??\"\",r+=o.nodeValue??\"\");r&&t.immediate.push(r),e instanceof Element&&e.shadowRoot&&(t.full+=createTextContent(e.shadowRoot).full),observedNodes.has(e)||(textChangeObserver.observe(e,{childList:!0,characterData:!0,subtree:!0}),observedNodes.add(e))}return textContentCache.set(e,t),t},TextQuerySelector_exports={};__export(TextQuerySelector_exports,{textQuerySelectorAll:()=>textQuerySelectorAll});var textQuerySelectorAll=function*(e,t){let r=!1;for(const o of e.childNodes)if(o instanceof Element&&isSuitableNodeForTextMatching(o)){let e;e=o.shadowRoot?textQuerySelectorAll(o.shadowRoot,t):textQuerySelectorAll(o,t);for(const t of e)yield t,r=!0}if(!r&&e instanceof Element&&isSuitableNodeForTextMatching(e)){createTextContent(e).full.includes(t)&&(yield e)}},util_exports={};__export(util_exports,{checkVisibility:()=>checkVisibility});var HIDDEN_VISIBILITY_VALUES=[\"hidden\",\"collapse\"],checkVisibility=(e,t)=>{if(!e)return!1===t;if(void 0===t)return e;const r=e.nodeType===Node.TEXT_NODE?e.parentElement:e,o=window.getComputedStyle(r);return t===(o&&!HIDDEN_VISIBILITY_VALUES.includes(o.visibility)&&!isBoundingBoxEmpty(r))&&e};function isBoundingBoxEmpty(e){const t=e.getBoundingClientRect();return 0===t.width||0===t.height}var XPathQuerySelector_exports={};__export(XPathQuerySelector_exports,{xpathQuerySelectorAll:()=>xpathQuerySelectorAll});var xpathQuerySelectorAll=function*(e,t){const r=(e.ownerDocument||document).evaluate(t,e,null,XPathResult.ORDERED_NODE_ITERATOR_TYPE);let o;for(;o=r.iterateNext();)yield o},PuppeteerUtil=Object.freeze({...ARIAQuerySelector_exports,...CustomQuerySelector_exports,...PierceQuerySelector_exports,...TextQuerySelector_exports,...util_exports,...XPathQuerySelector_exports,createDeferredPromise:createDeferredPromise,createFunction:createFunction,createTextContent:createTextContent,IntervalPoller:IntervalPoller,isSuitableNodeForTextMatching:isSuitableNodeForTextMatching,MutationPoller:MutationPoller,RAFPoller:RAFPoller}),injected_default=PuppeteerUtil;";
            }
            return _injectedSource;
        }

        private void Update(Action callback)
        {
            callback();
            _updated = true;
        }
    }
}

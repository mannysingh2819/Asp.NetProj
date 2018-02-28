using System;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using APFProductUpdateProj.SFDC;
using Oracle.DataAccess.Client;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace APFProductUpdateProj.Classes
{
    public class APFProduct
    {
        public int bc_product_id { get; set; }
        public int advertiser_id { get; set; }
        public int business_id { get; set; }
        public string product_status { get; set; }
        public string publish_ind { get; set; }
        public string product_code { get; set; }
        public string effective_version_ind { get; set; }
        public string publish_type { get; set; }
        public int main_source_id { get; set; }
        public string l5_heading_code { get; set; }
        public int l5_listing_id { get; set; }
        public string appearances { get; set; }
        public string content { get; set; }
        public string l5_directory_code { get; set; }
        public int l5_directory_issue { get; set; }
        public string attributes { get; set; }
        public string httpmsg { get; set; }

        public int listing_id { get; set; }
        public int listing_version { get; set; }
        public string atn_npa { get; set; }
        public string atn_cop { get; set; }
        public string atn_line_no { get; set; }
        public string listed_tn_npa { get; set; }
        public string listed_tn_cop { get; set; }
        public string listed_tn_line_no { get; set; }
        public string listed_name { get; set; }
        public string honorary_title { get; set; }
        public string lineage_title { get; set; }
        public string designation { get; set; }
        public string dial_inst { get; set; }
        public string right_justified_text { get; set; }
        public string cross_reference_text { get; set; }
        public string ali_code { get; set; }
        public string effective_date { get; set; }
        public int customer_id { get; set; }
        public int main_listing_id { get; set; }
        public string listing_status { get; set; }
        public string lni { get; set; }
        public string style_code { get; set; }
        public string brg_ind { get; set; }
        public string tn_type { get; set; }
        public string omit_address_ind { get; set; }
        public string advance_ind { get; set; }
        public string adv_completion_date { get; set; }
        public string pla { get; set; }
        public string directive_text { get; set; }
        public string rcf_ind { get; set; }
        public string open_reason { get; set; }
        public string transaction_code { get; set; }
        public string disconnect_reason { get; set; }
        public string so_type { get; set; }
        public string associate_ind { get; set; }
        public string yp_scoping_type { get; set; }
        public string follow_street_num { get; set; }
        public string follow_str_type_sfx { get; set; }
        public string street_num_prefix { get; set; }
        public string street_no { get; set; }
        public string street_name { get; set; }
        public string street_type_code { get; set; }
        public string street_directional { get; set; }
        public string apartment { get; set; }
        public string locality_id { get; set; }
        public string loc_name_override { get; set; }
        public string state_name_override { get; set; }
        public string country_name { get; set; }
        public string post_code { get; set; }
        public string po_box { get; set; }
        public string optional_address { get; set; }
        public string optional_addr_type { get; set; }
        public string postal_addr_type { get; set; }
        public string po_locality_id { get; set; }
        public string po_post_code { get; set; }
        public string dir_type { get; set; }
        public static string clientid { get; set; }
        public static string clientsecret { get; set; }
        public static string username { get; set; }
        public static string password { get; set; }
        public static string securitytoken { get; set; }
        public static string restEndPoint { get; set; }
        public static string oauthToken { get; set; }
        public static string serviceUrl { get; set; }

        public DataTable createOracleConnection(string sqlString)
        {

            string connString = ConfigurationManager.ConnectionStrings["dbc"].ConnectionString;
            using (OracleConnection OraConn = new OracleConnection(connString))
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = OraConn;
                cmd.CommandText = sqlString;
                cmd.CommandType = CommandType.Text;
                OraConn.Open();
                DataTable dts = new DataTable();
                OracleDataAdapter oda = new OracleDataAdapter(cmd.CommandText, OraConn);
                oda.Fill(dts);
                return dts;
            }

        }
        public DataTable getBulkDataTable(string ProductId,string Product_Attribute_Id)
        {
            string sqlString = "WITH BC_PROD AS " +
                                  "( " +
                                     "SELECT * FROM " +
                                     "( " +
                                  "SELECT " +
                                      "t.ADVERTISER_ID, " +
                                      "t.PRODUCT_ATTRIBUTE_ID, " +
                                      "t.PUBLISH_TYPE, " +
                                      "t.BC_PRODUCT_ID, " +
                                      "t.PRODUCT_CODE, " +
                                      "t.PRODUCT_STATUS, " +
                                      "t.PUBLISH_IND, " +
                                      "t.SENIORITY_DATE, " +
                                      "t.CTCR_SOURCE_PROG_ID,  " +
                                      "t.PRODUCT_VERSION, " +
                                      "t.EFFECTIVE_VERSION_IND, " +
                                      "t.REASON_TEXT, " +
                                      "t.L5_HEADING_CODE, " +
                                      "t.L5_LISTING_ID, " +
                                      "t.L5_DIRECTORY_CODE, " +
                                      "t.L5_DIRECTORY_ISSUE, " +
                                      "t.L5_MAIN_PRODUCT_ID, " +
                                      "t.PD_PRODUCT_VERSION, " +
                                      "ROW_NUMBER() OVER (PARTITION BY " +
                                      "t.ADVERTISER_ID, " +
                                      "t.PRODUCT_ATTRIBUTE_ID, " +
                                      "t.PUBLISH_TYPE, " +
                                      "t.BC_PRODUCT_ID, " +
                                      "t.PRODUCT_CODE, " +
                                      "t.PRODUCT_STATUS, " +
                                      "t.PUBLISH_IND, " +
                                      "t.PRODUCT_VERSION, " +
                                      "t.L5_LISTING_ID, " +
                                      "t.L5_DIRECTORY_CODE, " +
                                      "t.L5_DIRECTORY_ISSUE, " +
                                      "t.L5_MAIN_PRODUCT_ID, " +
                                      "t.PD_PRODUCT_VERSION ORDER BY t.PRODUCT_VERSION DESC) rn " +
                                     "FROM DLV_WORK.BC_PRODUCT t   " +
                                     ") WHERE rn=1 " +
                                   "), " +
                                  "BC_CONTENT AS( " +
                                  "SELECT d.CONTENT_ID,d.CONTENT_DATA FROM " +
                                  "ACMS_WORK.ACMS_CONTENT d " +
                                  " WHERE d.LAST_VERSION_IND = 'Y' " +
                                  "), " +
                                  "BC_ATT AS(  " +
                                    "SELECT    " +
                                       "q.PRODUCT_ATTRIBUTE_ID, " +
                                       "q.ATTRIBUTE_CODE || '@' || q.ATTRIBUTE_VALUE AS ATTRIBUTES,  " +
                                       "CASE WHEN (SELECT LENGTH(TRIM(TRANSLATE(SUBSTR(q.ATTRIBUTE_VALUE,0,2), ' +-.0123456789',' '))) FROM dual) IS NULL  THEN  " +
                                       "  SUBSTR(q.ATTRIBUTE_VALUE,0,INSTR(q.ATTRIBUTE_VALUE,'_',1)-1) ELSE NULL END AS Content_Id  " +
                                     "FROM " +
                                    "DLV_WORK.BC_PRODUCT_ATTRIBUTE q  " +
                                    "WHERE q.Product_Attribute_Id IN (" + Product_Attribute_Id + ")" +
                                    " ORDER BY Content_Id NULLS LAST" +
                                      ") " +

                                    "SELECT  " +
                                      "BC_PRODUCT_ID," +
                                      "L5_DIRECTORY_CODE, " +
                                      "ADVERTISER_ID, " +
                                      "L5_DIRECTORY_ISSUE, " +
                                      "L5_LISTING_ID, " +
                                      "L5_MAIN_PRODUCT_ID, " +
                                      "PRODUCT_CODE, " +
                                      "PUBLISH_TYPE, " +
                                      "PUBLISH_IND, " +
                                      "EFFECTIVE_VERSION_IND, " +
                                      "PRODUCT_STATUS, " +
                                      "L5_HEADING_CODE, " +
                                      "CONTENT_DATA, " +
                                      "DIR_TYPE " +
                                    "FROM " +
                                    "( " +
                                    "SELECT    " +
                                        "a.ADVERTISER_ID, " +
                                        "a.PUBLISH_TYPE, " +
                                        "a.BC_PRODUCT_ID, " +
                                        "a.PRODUCT_CODE, " +
                                        "CASE WHEN Substr(a.PRODUCT_CODE,0,2)='WP' THEN 'White Pages' ELSE 'Yellow Pages' END AS DIR_TYPE, " +
                                        "a.PRODUCT_STATUS, " +
                                        "a.PUBLISH_IND,  " +
                                        "a.PRODUCT_VERSION, " +
                                        "a.EFFECTIVE_VERSION_IND, " +
                                        "a.L5_HEADING_CODE, " +
                                        "a.L5_LISTING_ID, " +
                                        "a.L5_DIRECTORY_CODE, " +
                                        "a.L5_DIRECTORY_ISSUE, " +
                                        "a.L5_MAIN_PRODUCT_ID, " +
                                        "e.CONTENT_DATA, " +
                                        "row_number() OVER(PARTITION BY " +
                                        "a.ADVERTISER_ID, " +
                                        "a.PUBLISH_TYPE, " +
                                        "a.BC_PRODUCT_ID, " +
                                        "a.PRODUCT_CODE, " +
                                        "a.L5_DIRECTORY_CODE, " +
                                        "a.L5_DIRECTORY_ISSUE, " +
                                        "a.L5_MAIN_PRODUCT_ID ORDER BY a.L5_DIRECTORY_ISSUE,a.PRODUCT_VERSION  DESC  NULLS LAST " +
                                        ")rn " +
                                      "FROM  BC_PROD a " +
                                      "JOIN BC_ATT d " +
                                    "ON d.PRODUCT_ATTRIBUTE_ID = a.PRODUCT_ATTRIBUTE_ID " +
                                    " LEFT JOIN BC_CONTENT e " +
                                    "ON e.CONTENT_ID = d.CONTENT_ID   " +
                                       " WHERE a.BC_PRODUCT_ID IN(" + ProductId + ")  " +
                                     " AND a.EFFECTIVE_VERSION_IND = 'Y' " +
                                     " AND a.PRODUCT_STATUS != 'C' " +
                                     //  "AND a.PRODUCT_VERSION IN (" + productver +")" +
                                     ") WHERE rn=1 ";
            // "AND CONTENT_DATA IS NOT  NULL ";


            return createOracleConnection(sqlString);

        }
        public DataTable getDataTable(string ProductId )
        {
              string sqlString  = "WITH BC_PROD AS " +
                                    "( " +
                                       "SELECT * FROM " +
                                       "( " +
                                    "SELECT " +
                                        "t.ADVERTISER_ID, " +
                                        "t.PRODUCT_ATTRIBUTE_ID, " +
                                        "t.PUBLISH_TYPE, " +
                                        "t.BC_PRODUCT_ID, " +
                                        "t.PRODUCT_CODE, " +
                                        "t.PRODUCT_STATUS, " +
                                        "t.PUBLISH_IND, " +
                                        "t.SENIORITY_DATE, " +
                                        "t.CTCR_SOURCE_PROG_ID,  " +
                                        "t.PRODUCT_VERSION, " +
                                        "t.EFFECTIVE_VERSION_IND, " +
                                        "t.REASON_TEXT, " +
                                        "t.L5_HEADING_CODE, " +
                                        "t.L5_LISTING_ID, " +
                                        "t.L5_DIRECTORY_CODE, " +
                                        "t.L5_DIRECTORY_ISSUE, " +
                                        "t.L5_MAIN_PRODUCT_ID, " +
                                        "t.PD_PRODUCT_VERSION, " +
                                        "ROW_NUMBER() OVER (PARTITION BY " +
                                        "t.ADVERTISER_ID, " +
                                        "t.PRODUCT_ATTRIBUTE_ID, " +
                                        "t.PUBLISH_TYPE, " +
                                        "t.BC_PRODUCT_ID, " +
                                        "t.PRODUCT_CODE, " +
                                        "t.PRODUCT_STATUS, " +
                                        "t.PUBLISH_IND, " +
                                        "t.PRODUCT_VERSION, " +
                                        "t.L5_LISTING_ID, " +
                                        "t.L5_DIRECTORY_CODE, " +
                                        "t.L5_DIRECTORY_ISSUE, " +
                                        "t.L5_MAIN_PRODUCT_ID, " +
                                        "t.PD_PRODUCT_VERSION ORDER BY t.PRODUCT_VERSION DESC) rn " +
                                       "FROM DLV_WORK.BC_PRODUCT t   " +
                                       ") WHERE rn=1 " +
                                     "), " +
                                    "BC_CONTENT AS( " +
                                    "SELECT d.CONTENT_ID,d.CONTENT_DATA FROM " +
                                    "ACMS_WORK.ACMS_CONTENT d " +
                                    "), " +
                                    "BC_ATT AS(  " +
                                      "SELECT    " +
                                         "q.PRODUCT_ATTRIBUTE_ID, " +
                                         "q.ATTRIBUTE_CODE || '@' || q.ATTRIBUTE_VALUE AS ATTRIBUTES,  " +
                                         "CASE WHEN (SELECT LENGTH(TRIM(TRANSLATE(SUBSTR(q.ATTRIBUTE_VALUE,0,2), ' +-.0123456789',' '))) FROM dual) IS NULL  THEN  " +
                                         "  SUBSTR(q.ATTRIBUTE_VALUE,0,INSTR(q.ATTRIBUTE_VALUE,'_',1)-1) ELSE NULL END AS Content_Id  " +
                                       "FROM " +
                                      "DLV_WORK.BC_PRODUCT_ATTRIBUTE q  " +
                                        ") " +

                                      "SELECT  " +
                                        "BC_PRODUCT_ID," +
                                        "L5_DIRECTORY_CODE, " +
                                        "ADVERTISER_ID, " +
                                        "L5_DIRECTORY_ISSUE, " +
                                        "L5_LISTING_ID, " +
                                        "L5_MAIN_PRODUCT_ID, " +
                                        "PRODUCT_CODE, " +
                                        "PUBLISH_TYPE, " +
                                        "PUBLISH_IND, " +
                                        "EFFECTIVE_VERSION_IND, " +
                                        "PRODUCT_STATUS, " +
                                        "L5_HEADING_CODE, " +
                                        "CONTENT_DATA, " +
                                        "DIR_TYPE " +
                                      "FROM " +
                                      "( " +
                                      "SELECT    " +
                                          "a.ADVERTISER_ID, " +
                                          "a.PUBLISH_TYPE, " +
                                          "a.BC_PRODUCT_ID, " +
                                          "a.PRODUCT_CODE, " +
                                          "CASE WHEN Substr(a.PRODUCT_CODE,0,2)='WP' THEN 'White Pages' ELSE 'Yellow Pages' END AS DIR_TYPE, " +
                                          "a.PRODUCT_STATUS, " +
                                          "a.PUBLISH_IND,  " +
                                          "a.PRODUCT_VERSION, " +
                                          "a.EFFECTIVE_VERSION_IND, " +
                                          "a.L5_HEADING_CODE, " +
                                          "a.L5_LISTING_ID, " +
                                          "a.L5_DIRECTORY_CODE, " +
                                          "a.L5_DIRECTORY_ISSUE, " +
                                          "a.L5_MAIN_PRODUCT_ID, " +
                                          "e.CONTENT_DATA, " +
                                          "row_number() OVER(PARTITION BY " +
                                          "a.ADVERTISER_ID, " +
                                          "a.PUBLISH_TYPE, " +
                                          "a.BC_PRODUCT_ID, " +
                                          "a.PRODUCT_CODE, " +
                                          "a.L5_DIRECTORY_CODE, " +
                                          "a.L5_DIRECTORY_ISSUE, " +
                                          "a.L5_MAIN_PRODUCT_ID ORDER BY a.L5_DIRECTORY_ISSUE,a.PRODUCT_VERSION DESC " +
                                          ")rn " +
                                        "FROM  BC_PROD a " +
                                        "JOIN BC_ATT d " +
                                      "ON d.PRODUCT_ATTRIBUTE_ID = a.PRODUCT_ATTRIBUTE_ID " +
                                      "LEFT JOIN BC_CONTENT e " +
                                      "ON e.CONTENT_ID = d.CONTENT_ID   " +
                                         " WHERE a.BC_PRODUCT_ID IN(" + ProductId + ")  " +
                                       " AND a.EFFECTIVE_VERSION_IND = 'Y' " +
                                       " AND a.PRODUCT_STATUS != 'C' " +
                                       //  "AND a.PRODUCT_VERSION IN (" + productver +")" +
                                       ") WHERE rn=1 ";
                                      // "AND CONTENT_DATA IS NOT  NULL ";

               
                return createOracleConnection(sqlString);
     
        }

        public DataTable getContent(string ContentId)
        {
            
            if (!string.IsNullOrEmpty(ContentId)) { 
            StringBuilder sbldr = new StringBuilder();
            sbldr.Append("SELECT");
            sbldr.Append(" ");
            sbldr.Append("CONTENT_ID,CONTENT_DATA");
            sbldr.Append(" ");
            sbldr.Append("FROM ");
            sbldr.Append("( ");
            sbldr.Append("SELECT d.CONTENT_ID, d.CONTENT_DATA, d.CONTENT_VERSION,");
            sbldr.Append("ROW_NUMBER() OVER(PARTITION BY d.CONTENT_ID ORDER BY d.CONTENT_VERSION DESC) rn ");
            sbldr.Append(" ");
            sbldr.Append("FROM ");
            sbldr.Append(" ");
            sbldr.Append("ACMS_WORK.ACMS_CONTENT d");
            sbldr.Append(" ");
            sbldr.Append("WHERE d.CONTENT_ID IN (");
            sbldr.Append(ContentId);
            sbldr.Append(")");
            sbldr.Append(") ");
            sbldr.Append("WHERE rn = 1");

             string sqlString = sbldr.ToString();
             return createOracleConnection(sqlString);
            }
            else
            {
                return null;
            }
            
        }


        public string getAttributeString(string ProductId)
        {
            DataTable Atrb = getAttributes(ProductId);
            string atb = string.Empty;
            StringBuilder sbr = new StringBuilder();
            foreach (DataRow dr in Atrb.Rows)
            {
               
                sbr.Append(dr["PRODUCT_ATTRIBUTE_ID"].ToString());
                sbr.Append(",");
            }
            atb = sbr.ToString().Substring(0, sbr.ToString().LastIndexOf(",")); 
            return atb;
        }

            public DataTable getAttributes(string ProductId)
            {
            
                string sqlString = "WITH BC_PROD AS " +
                                    "(SELECT * FROM " +
                                       "( " +
                                    "SELECT " +
                                         "t.PRODUCT_ATTRIBUTE_ID, " +
                                         "t.PRODUCT_VERSION, " +
                                         "t.PD_PRODUCT_VERSION, " +
                                         "t.BC_PRODUCT_ID, " +
                                         "t.PRODUCT_STATUS, " +
                                         "ROW_NUMBER() OVER (PARTITION BY " +
                                         "t.PRODUCT_ATTRIBUTE_ID, " +
                                         "t.BC_PRODUCT_ID, " +
                                         "t.PRODUCT_VERSION, " +
                                         "t.PD_PRODUCT_VERSION ORDER BY t.PRODUCT_VERSION DESC) rn " +
                                       "FROM DLV_WORK.BC_PRODUCT t " +
                                       " WHERE t.EFFECTIVE_VERSION_IND ='Y' " +
                                       ") " +
                                       "WHERE rn=1 " +
                                    "), " +
                                    "BC_PROD_APP AS( " +
                                    "SELECT " +
                                        "p.PRODUCT_APPEAR_ID, " +
                                        "p.PRODUCT_ATTRIBUTE_ID, " +
                                        "p.APPEAR_GROUP_ID, " +
                                        "p.PRODUCT_CODE, " +
                                        "p.PD_PRODUCT_VERSION " +
                                       "FROM " +
                                    "DLV_WORK.BC_PRODUCT_APPEARANCE p " +
                                    "), " +
                                    "BC_ATT AS( " +
                                      "SELECT "+  
                                    "q.PRODUCT_ATTRIBUTE_ID, " +
                                         "q.ATTRIBUTE_CODE || '@' || q.ATTRIBUTE_VALUE AS ATTRIBUTES, " +
                                         "SUBSTR(q.ATTRIBUTE_VALUE,0,INSTR(q.ATTRIBUTE_VALUE,'_',1)-1) AS Content_Id " +
                                       "FROM  " +
                                       "DLV_WORK.BC_PRODUCT_ATTRIBUTE q  " +
                                        ") " +

                                       "SELECT " +
                                        "BC_PRODUCT_ID, " +
                                        "PRODUCT_ATTRIBUTE_ID, " +
                                        "ATTRIBUTES " +
                                      "FROM " +
                                      "( " +
                                        "SELECT " +
                                            "BC_PRODUCT_ID, " +
                                            "PRODUCT_ATTRIBUTE_ID, " +
                                            "ROW_NUMBER() OVER(PARTITION BY " +
                                             "BC_PRODUCT_ID " +
                                             "ORDER BY PRODUCT_VERSION DESC)rk, " +
                                            "LISTAGG(ATTRIBUTES, ', ') WITHIN GROUP (ORDER BY ATTRIBUTES) ATTRIBUTES " +
                                            "FROM " +
                                            "( " +
                                         "SELECT " +
                                           "a.PRODUCT_VERSION, " +
                                           "d.PRODUCT_ATTRIBUTE_ID, " +
                                           "a.BC_PRODUCT_ID, " +
                                           "d.CONTENT_ID, " +
                                          "LISTAGG(ATTRIBUTES, ', ') WITHIN GROUP (ORDER BY ATTRIBUTES) ATTRIBUTES " +
                                         "FROM  BC_PROD  a " +
                                       "JOIN BC_PROD_APP b " +
                                         "ON a.PRODUCT_ATTRIBUTE_ID = b.PRODUCT_ATTRIBUTE_ID " +
                                         "AND a.PD_PRODUCT_VERSION = b.PD_PRODUCT_VERSION " +
                                          "JOIN BC_ATT d " +
                                        "ON d.PRODUCT_ATTRIBUTE_ID = a.PRODUCT_ATTRIBUTE_ID " +
                                         " WHERE a.BC_PRODUCT_ID IN (" + ProductId + ")" +
                                       " AND a.PRODUCT_STATUS !='C' " +
                                          " GROUP BY  " +
                                         "a.PRODUCT_VERSION, " +
                                         "d.PRODUCT_ATTRIBUTE_ID, " +
                                           "a.BC_PRODUCT_ID, " +
                                           "d.CONTENT_ID " +
                                          ") " +
                                          "GROUP BY " +
                                          "PRODUCT_VERSION, " +
                                          "PRODUCT_ATTRIBUTE_ID, " +
                                          "BC_PRODUCT_ID " +
                                           ") WHERE rk=1";

                

                return createOracleConnection(sqlString);
    

        }
        public DataTable getApperances(string ProductId)
        {
            
            string sqlString = "WITH BC_PROD AS " +
                                        "( " +
                                        "SELECT " +
                                            "t.PRODUCT_ATTRIBUTE_ID, " +
                                            "t.EFFECTIVE_VERSION_IND," +
                                            "t.BC_PRODUCT_ID, " +
                                            "t.PRODUCT_VERSION, " +
                                            "t.PD_PRODUCT_VERSION, " +
                                            "t.PRODUCT_CODE, " +
                                            "t.PRODUCT_STATUS " +
                                           "FROM " +
                                             "DLV_WORK.BC_PRODUCT t " +
                                        "), " +
                                        "BC_PROD_APP AS( " +
                                        "SELECT  " +
                                            "p.PRODUCT_APPEAR_ID, " +
                                            "p.PRODUCT_ATTRIBUTE_ID, " +
                                            "p.APPEAR_GROUP_ID, " +
                                            "p.PRODUCT_CODE, " +
                                            "p.PD_PRODUCT_VERSION " +
                                           "FROM " +
                                             "DLV_WORK.BC_PRODUCT_APPEARANCE p " +
                                        "), " +
                                        "BC_APP AS( " +
                                           "SELECT  " +
                                             "q.PRODUCT_APPEAR_ID, " +
                                             "q.ATTRIBUTE_CODE || '@' || q.ATTRIBUTE_VALUE AS APPEARANCES " +
                                           "FROM " +
                                             "DLV_WORK.BC_APPEARANCES q  " +
                                          ") " +


                                          "SELECT " +
                                            "* " +
                                          "FROM " +
                                          "( " +
                                          "SELECT    " +
                                             "PRODUCT_ATTRIBUTE_ID,  " +
                                             "BC_PRODUCT_ID, " +
                                             "PRODUCT_VERSION, " +
                                             "APPEARANCES, " +
                                             "DIR_TYPE, " +
                                             "ROW_NUMBER() OVER(PARTITION BY   " +
                                             "BC_PRODUCT_ID " +
                                              "ORDER BY PRODUCT_VERSION DESC)rk " +
                                          "FROM " +
                                           "( " +
                                          "SELECT   " +
                                             "PRODUCT_ATTRIBUTE_ID,  " +
                                             "BC_PRODUCT_ID, " +
                                             "PRODUCT_VERSION, " +
                                             "DIR_TYPE, "+
                                             "LISTAGG(APPEARANCES, ', ') WITHIN GROUP (ORDER BY APPEARANCES) APPEARANCES " +
                                          "FROM " +
                                          "( " +
                                          "SELECT  " +
                                              "a.BC_PRODUCT_ID, " +
                                              "a.PRODUCT_ATTRIBUTE_ID,  " +
                                              "LISTAGG(APPEARANCES, ', ') WITHIN GROUP (ORDER BY APPEARANCES) APPEARANCES, " +
                                              "a.PRODUCT_VERSION, " +
                                              "CASE WHEN Substr(a.PRODUCT_CODE,0,2)='WP' THEN 'White Pages' ELSE 'Yellow Pages' END as DIR_TYPE "+
                                           "FROM  BC_PROD a " +
                                           "JOIN BC_PROD_APP b " +
                                             "ON a.PRODUCT_ATTRIBUTE_ID = b.PRODUCT_ATTRIBUTE_ID " +
                                             "AND a.PD_PRODUCT_VERSION = b.PD_PRODUCT_VERSION  " +
                                             "JOIN BC_APP c " +
                                            "ON b.PRODUCT_APPEAR_ID =c.PRODUCT_APPEAR_ID " +
                                            "AND b.PRODUCT_CODE = a.PRODUCT_CODE " +
                                             "WHERE a.BC_PRODUCT_ID IN (" + ProductId + ")"+
                                              " AND a.EFFECTIVE_VERSION_IND ='Y'  " +
                                             " AND a.PRODUCT_STATUS !='C' " +
                                            " GROUP BY " +
                                              "a.PRODUCT_ATTRIBUTE_ID, " +
                                              "a.BC_PRODUCT_ID, " +
                                              "a.PRODUCT_VERSION, " +
                                              "CASE WHEN Substr(a.PRODUCT_CODE,0,2)='WP' THEN 'White Pages' ELSE 'Yellow Pages' END" +
                                              " )  " +
                                              " GROUP BY PRODUCT_ATTRIBUTE_ID,  " +
                                                         "BC_PRODUCT_ID, " +
                                                         "PRODUCT_VERSION, " +
                                                         "DIR_TYPE " +
                                               ") " +
                                             ")  " +
                                              "WHERE RK=1";

        return createOracleConnection(sqlString);
    
        }
        public string getContentId()
        {
            string tr = "";
            StringBuilder sbd = new StringBuilder();
            Dictionary<string, string> dAtt = new Dictionary<string, string>();
            dAtt = getAttributes();
            foreach (string str in dAtt.Keys)
            {
                bool dictvalue = dAtt.TryGetValue(str, out tr);
                if (dictvalue)
                {
                    if ((tr.EndsWith("InfoText", System.StringComparison.CurrentCultureIgnoreCase) || tr.EndsWith("CaptionText", System.StringComparison.CurrentCultureIgnoreCase)))
                    {

                        sbd.Append(tr.Substring(0, tr.IndexOf('_')));
                        sbd.Append(",");
                    }

                }

            }
            tr = sbd.ToString();

            tr = (!string.IsNullOrEmpty(tr)) ? tr.Substring(0, tr.LastIndexOf(",")) : null;
            return tr;
        }


        public Dictionary<string,string> getAttributes()
        {
            string[] Att = new string[] { };
            Dictionary<string, string> dictAtt = new Dictionary<string, string>();
            string[] allAtt = attributes.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries);
            foreach(string s in allAtt)
            {
               dictAtt.Add(s.Substring(0,s.LastIndexOf('@')), s.Substring(s.LastIndexOf('@')+1));
            }
            return dictAtt;
        }
        public Dictionary<string, string> getAppearances()
        {
            string[] Att = new string[] { };
            Dictionary<string, string> dictAtt = new Dictionary<string, string>();
            string[] allAtt = appearances.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in allAtt)
            {

                dictAtt.Add(s.Substring(0, s.LastIndexOf('@')), s.Substring(s.LastIndexOf('@') + 1));
            }
            return dictAtt;
        }

        public DataTable getListingDataTable(string listingid)
        {
          string sqlString =        "SELECT " +
                                          "listing_id," +
                                          "listing_version," +
                                          "atn_npa," +
                                          "atn_cop, " +
                                          "atn_line_no," +
                                          "listed_tn_npa," +
                                          "listed_tn_cop," +
                                          "listed_tn_line_no, " +
                                          "listed_name," +
                                          "honorary_title," +
                                          "lineage_title," +
                                          "designation," +
                                          "dial_inst," +
                                          "right_justified_text," +
                                          "cross_reference_text," +
                                          "ali_code," +
                                          "effective_date," +
                                          "customer_id," +
                                          "main_listing_id," +
                                          "listing_status," +
                                          "Current_Status," +
                                          "publish_ind," +
                                          "lni, " +
                                          "style_code," +
                                          "brg_ind," +
                                          "tn_type," +
                                          "omit_address_ind," +
                                          "advance_ind," +
                                          "adv_completion_date," +
                                          "pla," +
                                          "directive_text," +
                                          "rcf_ind," +
                                          "open_reason," +
                                          "transaction_code," +
                                          "disconnect_reason," +
                                          "so_type," +
                                          "associate_ind," +
                                          "yp_scoping_type," +
                                          "follow_street_num," +
                                          "follow_str_type_sfx," +
                                          "street_num_prefix," +
                                          "street_no," +
                                          "street_name," +
                                          "street_type_code," +
                                          "street_directional, " +
                                          "apartment," +
                                          "locality_id," +
                                          "loc_name_override," +
                                          "state_name_override," +
                                          "country_name," +
                                          "post_code," +
                                          "po_box," +
                                          "optional_address," +
                                          "optional_addr_type," +
                                          "postal_addr_type," +
                                          "po_locality_id," +
                                          "po_post_code" +
                                     " FROM" +
                                       "(" +
                                    "SELECT  t.listing_id," +
                                            "t.listing_version, " +
                                            "t.atn_npa," +
                                            "t.atn_cop," +
                                            "t.atn_line_no," +
                                            "t.listed_tn_npa," +
                                            "t.listed_tn_cop," +
                                            "t.listed_tn_line_no," +
                                            "REGEXP_REPLACE(listed_name,'\\W',' ') AS listed_name," +
                                            "t.honorary_title," +
                                            "t.lineage_title," +
                                            "t.designation," +
                                            "t.dial_inst," +
                                            "t.right_justified_text," +
                                            "t.cross_reference_text," +
                                            "t.ali_code," +
                                            "NVL(TO_CHAR(effective_date,'DD/MM/RRRR'),TRUNC(SYSDATE)) AS effective_date," +
                                            "t.customer_id," +
                                            "t.main_listing_id," +
                                            "'D' AS listing_status," +
                                            " t.listing_status as Current_Status," +
                                            "t.publish_ind," +
                                            "t.lni," +
                                            "t.style_code," +
                                            "t.brg_ind," +
                                            "t.tn_type," +
                                            "t.omit_address_ind," +
                                            "t.advance_ind," +
                                            "TO_CHAR(t.adv_completion_date,'DD/MM/RRRR') AS adv_completion_date," +
                                            "t.pla," +
                                            "t.directive_text," +
                                            "t.rcf_ind," +
                                            "t.open_reason," +
                                            "'D' AS transaction_code," +
                                            "'POT' AS disconnect_reason," +
                                            "'D' AS so_type," +
                                            "t.associate_ind," +
                                            "t.yp_scoping_type," +
                                            "t.follow_street_num," +
                                            "t.follow_str_type_sfx," +
                                            "p.street_num_prefix," +
                                            "p.street_no," +
                                            "p.street_name," +
                                            "p.street_type_code," +
                                            "p.street_directional," +
                                            "p.apartment," +
                                            "p.locality_id," +
                                            "p.loc_name_override," +
                                            "p.state_name_override," +
                                            "p.country_name," +
                                            "p.post_code," +
                                            "p.po_box," +
                                            "p.optional_address," +
                                            "p.optional_addr_type," +
                                            "p.postal_addr_type," +
                                            "p.po_locality_id," +
                                            "p.po_post_code," +
                                            "row_number() OVER(PARTITION BY t.customer_id  ORDER BY listing_version DESC)rn " +
                                           " FROM" +
                                             " CYP_WORK.PB_LISTING t" +
                                               " JOIN CYP_WORK.PB_ADDRESS p" +
                                                " ON p.customer_id = t.customer_id" +
                                                " WHERE BRG_IND IN ('B') " +
                                                " AND t.listing_id IN (" + listingid + ")" +
                                             ")" +
                                             " WHERE rn = 1 AND Current_Status !='D' ";


                //  OracleDataReader dr = cmd.ExecuteReader();

                //while (dr.Read())
                //{
                //      listing_id           = Convert.ToInt32(dr["listing_id"]);
                //      listing_version      = Convert.ToInt32(dr["listing_version"]);
                //      atn_npa              = dr["atn_npa"].ToString();
                //      atn_cop              = dr["atn_cop"].ToString();
                //      atn_line_no          = dr["atn_line_no"].ToString();
                //      listed_tn_npa        = dr["listed_tn_npa"].ToString();
                //      listed_tn_cop        = dr["listed_tn_cop"].ToString();
                //      listed_tn_line_no    = dr["listed_tn_line_no"].ToString();
                //      listed_name          = dr["listed_name"].ToString();
                //      honorary_title       = dr["honorary_title"].ToString();
                //      lineage_title        = dr["lineage_title"].ToString();
                //      designation          = dr["designation"].ToString();
                //      dial_inst            = dr["dial_inst"].ToString();
                //      right_justified_text = dr["right_justified_text"].ToString();
                //      cross_reference_text = dr["cross_reference_text"].ToString();
                //      ali_code             = dr["ali_code"].ToString();
                //      effective_date       = dr["effective_date"].ToString();
                //      customer_id          = Convert.ToInt32(dr["customer_id"]);
                //      main_listing_id      = Convert.ToInt32(dr["main_listing_id"]);
                //      listing_status       = dr["listing_status"].ToString();
                //      lni                  = dr["lni"].ToString();
                //      style_code           = dr["style_code"].ToString();
                //      brg_ind              = dr["brg_ind"].ToString();
                //      tn_type              = dr["tn_type"].ToString();
                //      omit_address_ind     = dr["omit_address_ind"].ToString();
                //      advance_ind          = dr["advance_ind"].ToString();
                //      adv_completion_date  = dr["adv_completion_date"].ToString();
                //      pla                  = dr["pla"].ToString();
                //      directive_text       = dr["directive_text"].ToString();
                //      rcf_ind              = dr["rcf_ind"].ToString();
                //      open_reason          = dr["open_reason"].ToString();
                //      transaction_code     = dr["transaction_code"].ToString();
                //      disconnect_reason    = dr["disconnect_reason"].ToString();
                //      so_type              = dr["so_type"].ToString();
                //      associate_ind        = dr["associate_ind"].ToString();
                //      yp_scoping_type      = dr["yp_scoping_type"].ToString();
                //      follow_street_num    = dr["follow_street_num"].ToString(); 
                //      follow_str_type_sfx  = dr["follow_str_type_sfx"].ToString(); 
                //      street_num_prefix    = dr["street_num_prefix"].ToString();
                //      street_no            = dr["street_no"].ToString();
                //      street_name          = dr["street_name"].ToString(); 
                //      street_type_code     = dr["street_type_code"].ToString(); 
                //      street_directional   = dr["street_directional"].ToString();
                //      apartment            = dr["apartment"].ToString();
                //      locality_id          = dr["locality_id"].ToString(); 
                //      loc_name_override    = dr["loc_name_override"].ToString(); 
                //      state_name_override  = dr["state_name_override"].ToString();
                //      country_name         = dr["country_name"].ToString();
                //      post_code            = dr["post_code"].ToString(); 
                //      po_box               = dr["po_box"].ToString(); 
                //      optional_address     = dr["optional_address"].ToString(); 
                //      optional_addr_type   = dr["optional_addr_type"].ToString(); 
                //      postal_addr_type     = dr["postal_addr_type"].ToString();
                //      po_locality_id       = dr["po_locality_id"].ToString();
                //      po_post_code         = dr["po_post_code"].ToString(); 
                //}                           

                return createOracleConnection(sqlString);
     
        }

        public LoginResult loginSF()
        {
            string username = ConfigurationManager.AppSettings["sfUserName"];
            string password = ConfigurationManager.AppSettings["sfPwd"];
            string token = ConfigurationManager.AppSettings["sfSecurityToken"];
            SforceService sfService = new SforceService();
            LoginResult lgResult = sfService.login(username, password + token);
            return lgResult;
        }


        public QueryResult connectToSF(string productId)
        {
            string SQL = "Select Id,APF_Product_Id__c FROM Print_Line_Item__c Where APF_Product_Id__c IN (" + productId + ")";
            SforceService sfService = new SforceService();
            LoginResult lgResult = loginSF();
            sfService.Url = lgResult.serverUrl;
            sfService.SessionHeaderValue = new SessionHeader();
            sfService.SessionHeaderValue.sessionId = lgResult.sessionId;
            QueryResult queryResult = sfService.query(SQL);
            return queryResult;
       }
        public static async Task<HttpResponseMessage> PostXmlRequest(string baseUrl, string xmlString)
        {
            using (var httpClient = new HttpClient())
            {
                var httpContent = new StringContent(xmlString, Encoding.UTF8, "application/soap+xml");
                httpContent.Headers.Add("SOAPAction", "http://amdocs/iam/ADAPTOR_WS/UpdateProductSDF_1");
                var credentials = Encoding.ASCII.GetBytes(@"CORP\w17386:Pcgeek321!");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));

                return await httpClient.PostAsync(baseUrl, httpContent);
            }
        }
      
        public string createApfSoapString()
        {
            string soapString = string.Empty;
            Dictionary<string, string> dAtt = new Dictionary<string, string>();
            dAtt = getAttributes();
            soapString = createxmlstring();
            string t = "";
            string mapoutput = "";
            StringBuilder sbd = new StringBuilder();
            DataTable db = new DataTable();
            foreach (string str in dAtt.Keys)
            {
                bool dictvalue = dAtt.TryGetValue(str, out t);
                if (dictvalue)
                {
                    if ((t.EndsWith("InfoText", System.StringComparison.CurrentCultureIgnoreCase) || t.EndsWith("CaptionText", System.StringComparison.CurrentCultureIgnoreCase)) )
                    {
                        //&& !string.IsNullOrEmpty(content)
                       // t = string.Empty;

                        db = getContent(t.Substring(0,t.IndexOf('_')));
                        t = "<![CDATA[" + db.Rows[0]["CONTENT_DATA"].ToString() + "]]>";

                        //StringBuilder sbd = new StringBuilder();
                        //sbd.Append(@"<![CDATA[<i:ROOT xmlns:i=""http://amdocs/acms/content/InfoText"">");
                        //sbd.Append(@"<Content id=""" + t.Substring(0, t.IndexOf("_")) + @""" version=""1""><Definition><Code>0</Code><Version>1</Version></Definition><InfoText><AdvertiserCntInfo>");
                        //sbd.Append(@"<Ownership>" + advertiser_id + "</Ownership><PublishInd>" + publish_ind + "</PublishInd><EffectiveStartDate>2017-01-24</EffectiveStartDate><EffectiveEndDate>2099-12-31</EffectiveEndDate>");
                        //sbd.Append(@"<Authorization>12345</Authorization><ExcludePublishDestinations isSearch=""true"">str1234</ExcludePublishDestinations></AdvertiserCntInfo>");
                        //sbd.Append(@"<GeneralInfo><ContentId>0</ContentId><ContentVersion>1</ContentVersion><AdvertiserID>" + advertiser_id + "</AdvertiserID><AdvertiserInd>Y</AdvertiserInd>");
                        //sbd.Append(@"<AutoThumbnailGenerate>Y</AutoThumbnailGenerate><ContentStatus>A</ContentStatus></GeneralInfo><InfoText>www.phl.net.au</InfoText></InfoText>");
                        //sbd.Append(@"</Content></i:ROOT>]]>");
                        //t = sbd.ToString();
                    }
                    
                    sbd.Append("<attribute><attributeCode>" + str.Trim() + "</attributeCode><attributeValue>" + t.Trim() + "</attributeValue></attribute>");
                }
            }
            mapoutput = sbd.ToString();

            StringBuilder sb = new StringBuilder();
            // System.Diagnostics.Debug.Print(mapoutput);
            string replacedstring = soapString.Replace("@attribute", mapoutput);
            // System.Diagnostics.Debug.Print(replacedstring);

            Dictionary<string, string> dapp = new Dictionary<string, string>();
            dapp = this.getAppearances();
            string st = "";
            string smapoutput = "";
            foreach (string sr in dapp.Keys)
            {
                bool dictval = dapp.TryGetValue(sr, out st);
                if (dictval)
                {
                    sb.Append("<Attribute><Code>" + sr.Trim() + "</Code><Value>" + st.Trim() + "</Value></Attribute>");
                    
                }
            }
            smapoutput = sb.ToString();
        //  System.Diagnostics.Debug.Print(smapoutput);
            string rstring = replacedstring.Replace("@appearance", smapoutput);
            return rstring;
        }

        public async Task<HttpResponseMessage> CreateSoapEnvelop()
        {
            
            string sendSoapMessage = createApfSoapString();

            // System.Diagnostics.Debug.Print(rstring);

            HttpResponseMessage response = await PostXmlRequest("https://api.sensis.com.au/apf/xrrv6qytnuvquwt3eckbmjqb/UpdateProductSDF_EWS_WS/ADAPTOR", sendSoapMessage);

            //string content = await response.Content.ReadAsStringAsync();

            return response;
        }
        public static async Task<HttpResponseMessage> PostXmlRequest(string baseUrl, string xmlString, HttpContent httpContent)
        {
            using (var httpClient = new HttpClient())
            {
               
                httpContent.Headers.Add("SOAPAction", "http://amdocs/iam/ADAPTOR_WS/UpdateProductSDF_1");
                var credentials = Encoding.ASCII.GetBytes(@"CORP\w17386:Pcgeek321!");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));

                return await httpClient.PostAsync(baseUrl, httpContent);
            }
        }

        public async Task<HttpResponseMessage> CreateSoapEnvelop(HttpContent httpContent,string indicatorType)
        {
            HttpResponseMessage response;
            string soapString;

            if (indicatorType == "cancelProduct")
            {
                Dictionary<string, string> dAtt = new Dictionary<string, string>();
                dAtt = getAttributes();
                soapString = createxmlstring();
                string t = "";
                foreach (string str in dAtt.Keys)
                {
                    soapString.Replace("@attribute", "<attribute><attributeCode>" + str + "</attributeCode><attributeValue>" + dAtt.TryGetValue(str,out t));
                }
                

                 response = await PostXmlRequest("https://api.sensis.com.au/apf/xrrv6qytnuvquwt3eckbmjqb/UpdateProductSDF_EWS_WS/ADAPTOR", soapString, httpContent);
            }
            else
            {
                 soapString = CancelListingXml();

                 response = await PostXmlRequest("https://api.sensis.com.au/apf/xrrv6qytnuvquwt3eckbmjqb/UpdateListingSDF_EWS_WS/ADAPTOR", soapString, httpContent);
            }

            //string content = await response.Content.ReadAsStringAsync();

            return response;
        }
        public string createxmlstring()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/""");
            sb.Append(" ");
            sb.Append(@"xmlns:xsd=""http://www.w3.org/2001/XMLSchema""");
            sb.Append(" ");
            sb.Append(@"xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">");
            sb.Append(" ");
            sb.Append(@"<env:Header/><env:Body><UpdateProductSDF xmlns=""http://amdocs/iam/ADAPTOR_WS/UpdateProductSDF_1"">");
            sb.Append(@"<ROOT xmlns=""http://amdocs/iam/bc/CreUpdProductInput"">");
            sb.Append(@"<Event xmlns=""""><Type/><ID/></Event><DATA xmlns="""">");
            sb.Append(@"<staticProductEntity><advertiserId>" + advertiser_id + "</advertiserId>");
            sb.Append(@"<publishType>P</publishType>");
            sb.Append(@"<bcProductId>" + bc_product_id + "</bcProductId>");
            sb.Append(@"<productCode>" + product_code + "</productCode>");
            sb.Append(@"<status>" + product_status + "</status>");
            sb.Append(@"<publishInd>" + publish_ind + "</publishInd>");
            sb.Append(@"<baseProductId></baseProductId>");
            sb.Append(@"<seniorityDate><dateTimeString></dateTimeString></seniorityDate>");
            sb.Append(@"<ctcrSourceProgId>SDFUPDATE</ctcrSourceProgId>");
            sb.Append(@"@attribute");
            sb.Append(@"<appearance><GroupID>1</GroupID>");
            sb.Append(@"@appearance");
            sb.Append(@"<Attribute><Code>DIRTYPE</Code><Value>"+ dir_type +"</Value></Attribute><CUSTOM/></appearance>");
            sb.Append(@"<CUSTOM><effectiveVersionInd>" + effective_version_ind + "</effectiveVersionInd>");
            sb.Append(@"<reasonText></reasonText>");
            sb.Append(@"<L5_HEADING_CODE>" + l5_heading_code + "</L5_HEADING_CODE>");
            sb.Append(@"<L5_LISTING_ID>" + l5_listing_id + "</L5_LISTING_ID>");
            sb.Append(@"<L5_DIRECTORY_CODE>" + l5_directory_code + "</L5_DIRECTORY_CODE>");
            sb.Append(@"<L5_DIRECTORY_ISSUE>" + l5_directory_issue + "</L5_DIRECTORY_ISSUE>");
            sb.Append(@"<L5_MAIN_PRODUCT_ID>" + main_source_id + "</L5_MAIN_PRODUCT_ID>");
            sb.Append(@"</CUSTOM></staticProductEntity></DATA></ROOT></UpdateProductSDF></env:Body></env:Envelope>");
            
            return sb.ToString();
        }
        public string CancelListingXml()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">");
            sb.Append("<env:Header /><env:Body>");
            sb.Append(@"<UpdateListingSDF xmlns=""http://amdocs/iam/ADAPTOR_WS/UpdateListingSDF_1""");
            sb.Append(@"<ROOT xmlns=""http://amdocs/iam/cyp/pubint/service/CreateListingSDFInput""");
            sb.Append(@"<Event xmlns="""" /><DATA xmlns=""><generalTxParams /><transactionParams>");
            sb.Append(@"<listingEntity><listingPk><listingId>"+ listing_id + "</listingId>");
            sb.Append(@"<listingVersion>" + listing_version + "</listingVersion></listingPk>");
            sb.Append(@"<atnNpa>" + atn_npa + "</atnNpa><atnCop>" + atn_cop + "</atnCop><atnLineNo>" + atn_line_no + "</atnLineNo>");
            sb.Append(@"<listedTnNpa>" + listed_tn_npa + "</listedTnNpa>");
            sb.Append(@"<listedTnCop>" + listed_tn_cop + "</listedTnCop>");
            sb.Append(@"<listedTnLineNo>" + listed_tn_line_no + "</listedTnLineNo>");
            sb.Append(@"<nonStandardTn xsi:nil=""true"" />");
            sb.Append(@"<headingCode xsi:nil=""true"" />");
            sb.Append(@"<listedName>" + listed_name + "</listedName>");
            sb.Append(@"<honoraryTitle>" + honorary_title + "</honoraryTitle>");
            sb.Append(@"<lineageTitle>" + lineage_title + "</lineageTitle>");
            sb.Append(@"<designation>" + designation + "</designation>");
            sb.Append(@"<dialInst>" + dial_inst + "</dialInst>");
            sb.Append(@"<rightJustifiedText>" + right_justified_text + "</rightJustifiedText>");
            sb.Append(@"<crossReferenceText>" + cross_reference_text + "</crossReferenceText>");
            sb.Append(@"<webAddress xsi:nil=""true"" />");
            sb.Append(@"<emailAddress xsi:nil=""true"" />");
            sb.Append(@"<aliCode>" + ali_code + "</aliCode>");
            sb.Append(@"<effectiveDate><dateTimeString>" + effective_date + "</dateTimeString></effectiveDate>");
            sb.Append(@"<customerId>" + customer_id + "</customerId>");
            sb.Append(@"<mainListingId>" + main_listing_id + "</mainListingId>");
            sb.Append(@"<listingStatus>" + listing_status + "</listingStatus>");
            sb.Append(@"<publishInd>" + publish_ind + "</publishInd>");
            sb.Append(@"<lni>" + lni + "</lni>");
            sb.Append(@"<styleCode>" + style_code + "</styleCode>");
            sb.Append(@"<brgInd>" + brg_ind + "</brgInd>");
            sb.Append(@"<tnType>" + tn_type + "</tnType>");
            sb.Append(@"<omitAddressInd>" + omit_address_ind + "</omitAddressInd>");
            sb.Append(@"<advanceInd>" + advance_ind + "</advanceInd>");
            sb.Append(@"<advCompletionDate><dateTimeString>" + adv_completion_date + "</dateTimeString></advCompletionDate>");
            sb.Append(@"<pla>" + pla + "</pla>");
            sb.Append(@"<directiveText>" + directive_text + "</directiveText>");
            sb.Append(@"<rcfInd>" + rcf_ind + "</rcfInd>");
            sb.Append(@"<openReason>" + open_reason + "</openReason>");
            sb.Append(@"<transactionCode>" + transaction_code + "</transactionCode>");
            sb.Append(@"<disconnectReason>" + disconnect_reason + "</disconnectReason>");
            sb.Append(@"<soType>" + so_type + "</soType>");
            sb.Append(@"<associateInd>" + associate_ind + "</associateInd>");
            sb.Append(@"<asscDisassCustId xsi:nil=""true"" />");
            sb.Append(@"<ypScopingEntInd xsi:nil=""true"" />");
            sb.Append(@"<ypScopingType>" + yp_scoping_type + "</ypScopingType>");
            sb.Append(@"<followStreetNum>" + follow_street_num + "</followStreetNum>");
            sb.Append(@"<followStrTypeSfx>" + follow_str_type_sfx + "</followStrTypeSfx>");
            sb.Append(@"</listingEntity>");
            sb.Append(@"<addressEntity>");
            sb.Append(@"<listedAddressId xsi:nil=""true"" />");
            sb.Append(@"<streetNumPrefix>" + street_num_prefix + "</streetNumPrefix>");
            sb.Append(@"<streetNo>" + street_no + "</streetNo>");
            sb.Append(@"<streetName>" + street_name + "</streetName>");
            sb.Append(@"<streetTypeCode>" + street_type_code + "</streetTypeCode>");
            sb.Append(@"<streetDirectional>" + street_directional + "</streetDirectional>");
            sb.Append(@"<apartment>" + apartment + "</apartment>");
            sb.Append(@"<localityId>" + locality_id + "</localityId>");
            sb.Append(@"<locNameOverride>" + loc_name_override + "</locNameOverride>");
            sb.Append(@"<stateNameOverride>" + state_name_override + "</stateNameOverride>");
            sb.Append(@"<countryName>" + country_name + "</countryName>");
            sb.Append(@"<postCode>" + post_code + "</postCode>");
            sb.Append(@"<poBox>" + po_box + "</poBox>");
            sb.Append(@"<optionalAddress>" + optional_address + "</optionalAddress>");
            sb.Append(@"<optionalAddrType>" + optional_addr_type + "</optionalAddrType>");
            sb.Append(@"<postalAddrType>" + postal_addr_type + "</postalAddrType>");
            sb.Append(@"<poLocalityId>" + po_locality_id + "</poLocalityId>");
            sb.Append(@"<poPostCode>" + po_post_code + "</poPostCode>");
            sb.Append(@"<longitude xsi:nil=""true"" />");
            sb.Append(@"<latitude xsi:nil=""true"" />");
            sb.Append(@"</addressEntity>");
            sb.Append(@"</transactionParams>");
            sb.Append(@"</DATA>");
            sb.Append(@"</ROOT>");
            sb.Append(@"</UpdateListingSDF>");
            sb.Append(@"</env:Body>");
            sb.Append(@"</env:Envelope>");
            
            return sb.ToString();
        }
        public async Task<string> handleRestapiCall(string restQuery, HttpMethod httpmethod)
        {
            
            HttpClient hpc = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(httpmethod, restQuery);
            request.Headers.Add("Authorization", "Bearer " + oauthToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await hpc.SendAsync(request);
            string result = await response.Content.ReadAsStringAsync();
            return result;
           
        }


        public async Task<string> gettoken()
        {
            clientid = "3MVG9Y6d_Btp4xp7DH1xCUP.HnfWyfDVn1F9pXZ7Lam9ylOqxsIgdxvQemN62Lvb.WJjg4tL8uhaOhsAjBHQb";
            clientsecret = "7636787925380816024";
            username = "salesforcesupport@sensis.com.au.newco";
            securitytoken = "2BGfGshu6l83VzxRG4Spfw3P";
            password = "XsCyJ6mNb8Nl2qfiWYcpFCeST" + securitytoken;

            restEndPoint = "https://login.salesforce.com/services/oauth2/token";
            HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type","password"},
                {"client_id",clientid},
                {"client_secret",clientsecret},
                {"username",username},
                {"password",password}
            }
            );
            HttpClient hpc = new HttpClient();
            HttpResponseMessage message = await hpc.PostAsync(restEndPoint, content);
            string responseString = await message.Content.ReadAsStringAsync();
            JObject obj = JObject.Parse(responseString);
            oauthToken = (string)obj["access_token"];
            serviceUrl = (string)obj["instance_url"];
            return oauthToken + "|" + serviceUrl;
        }

        public async Task<string> queryApexLog()
        {
            string query = "SELECT+Id+FROM+ApexLog+WHERE+Request+=++'API'+AND+Location+=+'SystemLog'+AND+Operation+like+'%25executeAnonymous%25'+AND+LogUserId+=+'00590000002BMZr'+ORDER+BY+StartTime+DESC,+Id+DESC+LIMIT+1";
            string soqlQuery = serviceUrl + "/services/data/v34.0/query/?q=" + query;

            await setTraceFlagAsync();

            string result = await handleRestapiCall(soqlQuery, HttpMethod.Get);
            JObject obj = JObject.Parse(result);
            string Id = string.Empty;
            if (obj["records"].HasValues) { 
                Id = (string)obj["records"][0]["Id"];
            }
            
            return Id;
        }

        public async Task<string> setTraceFlagAsync()
        {
        
            HttpClient hpc = new HttpClient();
            string restQuery = serviceUrl + "/services/data/v34.0/tooling/sobjects/traceFlag";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, restQuery);
            string requestMessage = "{\"ApexCode\": \"Debug\",\"ApexProfiling\": \"Error\",\"Callout\": \"Error\",\"Database\": \"Error\",\"ExpirationDate\" : \"" + DateTime.Today.AddDays(1).AddMinutes(120).ToUniversalTime().ToString("s") +  "\", \"TracedEntityId\": \"00590000002BMZr\",\"Validation\": \"Error\",\"Visualforce\": \"Error\",\"Workflow\": \"Error\",\"ScopeId\": null,\"System\": \"Error\"}";
            HttpContent jcontent = new StringContent(requestMessage, Encoding.UTF8, "application/JSON");
            request.Content = jcontent;
            request.Headers.Add("Authorization", "Bearer " + oauthToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await hpc.SendAsync(request);
            string result = await response.Content.ReadAsStringAsync();
            return result;

        }

        public async Task<string> rawlogs()
        {
            string result=string.Empty;
            string id = await queryApexLog();
            if (id.Length == 18)
            {
                string restQuery = serviceUrl + "/services/data/v34.0/sobjects/ApexLog/" + id + "/Body";
                result = await handleRestapiCall(restQuery, HttpMethod.Get);
            }
            else {
                result="The script was exected but could not get the debug log";
            }
            return result;
            
        }

        public async Task<string> connectRestAPIAsync(string executeAnonymous)
        {
            
            await gettoken();
            executeAnonymous = executeAnonymous.Replace("+", "%2B");
            string restQuery = serviceUrl + "/services/data/v34.0/tooling/executeAnonymous/?anonymousBody=" + executeAnonymous;
            string result = await handleRestapiCall(restQuery, HttpMethod.Get);
            JObject obj = JObject.Parse(result);
            return result;

            
        }
    }
}